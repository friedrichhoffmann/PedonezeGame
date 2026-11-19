using System.Collections.Generic;
using System.Linq;
using System;
using Godot;

using BookApiFile;
using BookLayout;

using EventSystem;
using static EventSystem.EventHandler;

public partial class LayoutManager : HBoxContainer
{
    [Export] public float BottomMargin { get; set; } = 24f;
    [Export] public string PagesDirectory { get; set; } = "";
    [Export] public BookStyle Style { get; set; }

    private Control _leftClip;
    private Control _rightClip;
    private BoxContainer _leftPage;
    private BoxContainer _rightPage;
    private IReadOnlyList<List<RenderItem>> _pages;

    private MarkdownBookLoader _loader;
    private BookDocument _document;

    private PageRenderer _renderer;
    private int _leftPageIndex;

    public override void _Ready() {
        _leftClip = GetNode<Control>("LeftClip");
        _rightClip = GetNode<Control>("RightClip");

        _leftPage = _leftClip.GetNode<BoxContainer>("LeftPage");
        _rightPage = _rightClip.GetNode<BoxContainer>("RightPage");

        _leftPage.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
        _rightPage.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);

        ClipContents = true;
        _leftClip.ClipContents = true;
        _rightClip.ClipContents = true;

        CallDeferred(nameof(LoadPaginateAndRender));
    }

    private void LoadPaginateAndRender() {
        if (Style == null) {
            GD.PushError("Assign a BookStyle in the LayoutManager Inspector.");
            return;
        }
        if (_leftClip.Size.X <= 0f || _leftClip.Size.Y <= 0f) {
            GD.PushError("Page clips have no size. Check the scene layout and anchors.");
            return;
        }

        _loader = new MarkdownBookLoader();
        _document = _loader.Load(PagesDirectory);

        var paginator = new PagePaginator(Style);

        float gap = _leftPage.GetThemeConstant("separation", "BoxContainer");

        _pages = paginator.Paginate(
                _document,
                _leftPage.Size.X,
                _leftPage.Size.Y,
                gap);


        GD.Print($"left page={_leftPage.Size}, right page={_rightPage.Size}");

        _renderer = new PageRenderer(
                _leftPage,
                _rightPage,
                _leftClip,
                _rightClip,
                Style,
                _document.Answers);

        _renderer.AnswerSolved += HandleAnswerSolved;

        _leftPageIndex = 0;
        ThrowEvent(new Event(
           "CloseBook",
           EventKind.Animation,
           EventBody.Animation(Style.AnimationPlayer, "CloseBook", 100)
        ));
        ShowSpread();
        
        ThrowEvent(new Event(
           "CloseBook",
           EventKind.Animation,
           EventBody.Animation(Style.AnimationPlayer, "OpenBook", 100)
        ));

        GD.Print($"pages={_pages.Count}");
        for (int i = 0; i < _pages.Count; i++)
            GD.Print($"page {i}: items={_pages[i].Count}, textHeight=" +
                    $"{_pages[i].Sum(item => paginator.MeasureTextHeight(item.Text, _leftPage.Size.X))}");

        GD.Print($"left children={_leftPage.GetChildCount()}, right children={_rightPage.GetChildCount()}");
    }

    private void HandleAnswerSolved(int answerIndex) {
        if (_loader == null)
            return;

        _document = _loader.RevealAnswer(answerIndex);

        var paginator = new PagePaginator(Style);
        float gap = _leftPage.GetThemeConstant("separation", "BoxContainer");

        _pages = paginator.Paginate(
                _document,
                _leftPage.Size.X,
                _leftPage.Size.Y,
                gap);

        _leftPageIndex = Math.Min(_leftPageIndex, Math.Max(0, _pages.Count - 1));

        _renderer = new PageRenderer(
                _leftPage,
                _rightPage,
                _leftClip,
                _rightClip,
                Style,
                _document.Answers);

        _renderer.AnswerSolved += HandleAnswerSolved;
        ShowSpread();
    }

    
    private void ShowSpread() {
        if (_renderer == null || _pages == null)
            return;

        _renderer.Render(
                _pages,
                _leftPageIndex,
                Style.TiltDegrees,
                -Style.TiltDegrees);

        GD.Print($"Showing pages {_leftPageIndex + 1}–" +
                $"{Math.Min(_leftPageIndex + 2, _pages.Count)} of {_pages.Count}");
    }

    private void TurnForward() {
        if (_pages == null || _leftPageIndex + 2 >= _pages.Count)
            return;

        _leftPageIndex += 2;
        ThrowEvent(new Event(
           "CloseBook",
           EventKind.Animation,
           EventBody.Animation(Style.AnimationPlayer, "CloseBook", 100)
        ));
        ShowSpread();
        
        ThrowEvent(new Event(
           "CloseBook",
           EventKind.Animation,
           EventBody.Animation(Style.AnimationPlayer, "OpenBook", 100)
        ));
    }

    private void TurnBackward() {
        if (_pages == null || _leftPageIndex == 0)
            return;

        _leftPageIndex = Math.Max(0, _leftPageIndex - 2);
        ThrowEvent(new Event(
           "CloseBook",
           EventKind.Animation,
           EventBody.Animation(Style.AnimationPlayer, "CloseBook", 100)
        ));
        ShowSpread();
        
        ThrowEvent(new Event(
           "CloseBook",
           EventKind.Animation,
           EventBody.Animation(Style.AnimationPlayer, "OpenBook", 100)
        ));
    }

    public override void _UnhandledKeyInput(InputEvent @event) {
        if (@event is not InputEventKey key || !key.Pressed || key.Echo)
            return;

        switch (key.Keycode) {
            case Key.Pagedown:
            case Key.Right:
                TurnForward();
                GetViewport().SetInputAsHandled();
                break;

            case Key.Pageup:
            case Key.Left:
                TurnBackward();
                GetViewport().SetInputAsHandled();
                break;
        }
    }

}

