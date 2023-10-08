using Cysharp.Threading.Tasks;

namespace Interfaces.Processes
{
    public interface IEndMainGameProcess
    {
        public UniTask EndMainGame();
    }
}