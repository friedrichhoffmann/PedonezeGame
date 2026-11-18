
using Godot;

public partial class jester_script : CharacterBody2D
{
	[Export]
	public Godot.Collections.Array<string> DialogueMessages { get; set; } = new();

	[Export]
	public float MessageInterval { get; set; } = 5.0f;

	private Area2D _detectionArea;
	private Node2D _dialogueBubble;
	private RichTextLabel _dialogueLabel;
	private AnimatedSprite2D _animatedSprite;
	private Timer _dialogueTimer;

	private int _currentMessageIndex = 0;
	private bool _playerInside = false;

	public override void _Ready()
	{
		_detectionArea = GetNode<Area2D>("DetectionArea");
		_dialogueBubble = GetNode<Node2D>("DialogueBubble");
		_dialogueLabel = GetNode<RichTextLabel>("DialogueBubble/Sprite2D2/RichTextLabel");
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		_dialogueTimer = new Timer();
		AddChild(_dialogueTimer);

		_dialogueTimer.WaitTime = MessageInterval;
		_dialogueTimer.OneShot = false;
		_dialogueTimer.Timeout += OnDialogueTimerTimeout;

		_dialogueBubble.Hide();

		_detectionArea.BodyEntered += OnBodyEntered;
		_detectionArea.BodyExited += OnBodyExited;

		_animatedSprite.AnimationFinished += OnAnimationFinished;

		_animatedSprite.SpriteFrames.SetAnimationLoop("Start", false);
		_animatedSprite.SpriteFrames.SetAnimationLoop("Rolling", true);

		_animatedSprite.Play("Front");
	}

	private void OnBodyEntered(Node2D body)
	{
		if (!body.IsInGroup("player") || _playerInside)
			return;

		_playerInside = true;
		StartDialogue();
	}

	private void OnBodyExited(Node2D body)
	{
		if (!body.IsInGroup("player"))
			return;

		_playerInside = false;
		EndDialogue();
	}

	private void StartDialogue()
	{
		_currentMessageIndex = 0;

		_dialogueBubble.Show();
		_animatedSprite.Play("Start");

		if (DialogueMessages.Count == 0)
			return;

		ShowCurrentMessage();
		_dialogueTimer.Start();
	}

	private void OnDialogueTimerTimeout()
	{
		_currentMessageIndex++;

		if (_currentMessageIndex >= DialogueMessages.Count)
		{
			_dialogueTimer.Stop();
			_dialogueBubble.Hide();
			return;
		}

		ShowCurrentMessage();
	}

	private void ShowCurrentMessage()
	{
		_dialogueLabel.Text = DialogueMessages[_currentMessageIndex];
	}

	private void EndDialogue()
	{
		_dialogueTimer.Stop();
		_dialogueBubble.Hide();

		_currentMessageIndex = 0;

		_animatedSprite.Play("Front");
	}

	private void OnAnimationFinished()
	{
		if (_animatedSprite.Animation == "Start" && _playerInside)
			_animatedSprite.Play("Rolling");
	}
}
