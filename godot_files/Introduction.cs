
using Godot;

public partial class Introduction : Label3D
{
	[Export] public float Speed = 1.0f;
	[Export] public float EndPosition = 30.0f;

	private Vector3 _startPosition;

	public override void _Ready()
	{
		Text = @"Pedoneze Game — As Crônicas de Roma

		Há muito tempo, em uma era distante... ou melhor, há muito tempo, em uma era que você jamais imaginou testemunhar, uma terrível maldição foi lançada sobre você por Zaratustra. Condenado a vagar pelas eras, você foi arrancado de seu próprio tempo e enviado diretamente ao coração de uma das maiores civilizações da história: o Império Romano.

		Ao despertar, você se encontra em meio a ruas movimentadas, monumentos colossais e legiões marchando sob estandartes imperiais. O ano é desconhecido, mas uma coisa é certa: Roma está no auge de seu poder. Entretanto, sua chegada não foi obra do acaso. Zaratustra deixou sobre seus pertences um antigo livro, cujas páginas estão repletas de lacunas, nomes esquecidos e acontecimentos incompletos.

		Uma antiga profecia revela que somente aquele capaz de reconstruir as origens de Roma poderá romper a maldição. Para isso, você deverá desvendar os mistérios de sua fundação, conhecer as lendas de Rômulo e Remo, acompanhar a formação dos primeiros povos e compreender como a cidade foi governada por seus reis até o nascimento da República. Cada descoberta será um fragmento essencial para reconstruir a história de uma das civilizações mais influentes da humanidade.

		Mas o caminho não será simples. Cada página incompleta representa um acontecimento fundamental da história romana. Sem os nomes, datas e fatos corretos, o livro permanecerá incompleto, e a maldição continuará intacta. O conhecimento será sua única arma contra a passagem implacável do tempo.

		Sua única esperança é encontrar uma misteriosa bruxa que habita os limites do Império. Dizem que ela conhece os segredos de Zaratustra e possui o poder de desfazer até mesmo as maldições mais antigas. Contudo, ela não aceitará o livro enquanto sua história não estiver inteiramente reconstruída.

		Agora, o destino está em suas mãos. Entre imperadores ambiciosos, generais lendários, intrigas senatoriais e povos conquistados, você deverá reunir os fragmentos de uma civilização que dominou o mundo antigo.

		Complete o livro. Reconstrua a história. Quebre a maldição.

		Que os deuses de Roma estejam ao seu lado.";

		Modulate = Colors.Yellow;

		RotationDegrees = new Vector3(-45, 0, 0);

		_startPosition = Position;
	}

	public override void _Process(double delta)
	{
		Position += GlobalBasis.Y * Speed * (float)delta;

		if (Position.Y >= _startPosition.Y + EndPosition)
		{
			GetTree().ChangeSceneToFile("res://mapa1.tscn");
		}
	}
}
