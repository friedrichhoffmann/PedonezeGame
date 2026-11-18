
using Godot;

public partial class npc_script : CharacterBody2D
{
	public enum FacingDirection
	{
		Front,
		Left,
		Right,
		Back,
		Shine,
		Working,
		Idle
	}

	[Export]
	public Godot.Collections.Array<string> DialogueMessages { get; set; } = new();

	[Export]
	public FacingDirection Direction { get; set; } = FacingDirection.Front;

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

		PlayFacingAnimation();
	}

	private void OnBodyEntered(Node2D body)
	{
		if (!body.IsInGroup("player"))
			return;

		if (_playerInside)
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
		if (DialogueMessages.Count == 0)
			return;

		_currentMessageIndex = 0;

		_dialogueBubble.Show();
		ShowCurrentMessage();

		if (Direction == FacingDirection.Front)
			_animatedSprite.Play("Shine");

		_dialogueTimer.Start();
	}

	private void OnDialogueTimerTimeout()
	{
		_currentMessageIndex++;

		if (_currentMessageIndex >= DialogueMessages.Count)
		{
			EndDialogue();
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

		PlayFacingAnimation();
	}

	private void OnAnimationFinished()
	{
		if (_animatedSprite.Animation == "Shine" && _playerInside)
			PlayFacingAnimation();
	}

	private void PlayFacingAnimation()
	{
		_animatedSprite.FlipH = false;

		switch (Direction)
		{
			case FacingDirection.Front:
				_animatedSprite.Play("Front");
				break;

			case FacingDirection.Left:
				_animatedSprite.Play("Left");
				break;

			case FacingDirection.Right:
				_animatedSprite.FlipH = true;
				_animatedSprite.Play("Left");
				break;

			case FacingDirection.Shine:
				_animatedSprite.Play("Shine");
				break;

			case FacingDirection.Back:
				_animatedSprite.Play("Back");
				break;

			case FacingDirection.Working:
				_animatedSprite.Play("Working");
				break;

			case FacingDirection.Idle:
				_animatedSprite.Play("Idle");
				break;
		}
	}
}
