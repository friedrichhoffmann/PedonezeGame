using Godot;
using InventoryAPIFile;
using SimpleItems;
using System;

public partial class Witch : npc_script
{
	[Export]
	public int RomulusBookId { get; set; }

	private InventoryAPI _inventory;
    [Export]
    public string[] _noBookLines = Array.Empty<string>();

	public override void _Ready()
	{
		_inventory = new InventoryAPI();
		_inventory.OpenInventory();

		base._Ready();
	}


    protected override void PrepareDialogue()
    {
        Item book = _inventory?.GetItem(RomulusBookId);

        if (book != null)
        {
            DialogueMessages = new Godot.Collections.Array<string>
            {
                "Entao voce conseguiu, uh?",
                "Bem, devo cumprir com aquilo que prometi",
                "Eu te liberto"
            };
            return;
        }

        if (_noBookLines == null || _noBookLines.Length == 0)
        {
            GD.PushWarning("Witch has no 'no book' dialogue lines configured.");
            DialogueMessages = new Godot.Collections.Array<string>
            {
                "Ainda estou esperando o livro."
            };
            return;
        }

        string line = _noBookLines[Random.Shared.Next(_noBookLines.Length)];
        DialogueMessages = new Godot.Collections.Array<string> { line };
    }

	public override void _ExitTree()
	{
		_inventory?.CloseInventory();
	}
}

