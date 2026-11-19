using System.Threading.Tasks;
using Godot;

namespace ClockFile;
public partial class Clock : Node {
    public static int Tick { get; protected set; }
   
    public override void _Ready() {
        _ = Ticking();
    }

    private async Task Ticking() {
        while (true) {
            Tick++;
            await Task.Delay(1);
        } 
    }
}
