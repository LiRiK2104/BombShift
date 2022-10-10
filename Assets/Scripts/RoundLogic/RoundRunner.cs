using RoundLogic.Finish;
using RoundLogic.Start;
using UnityEngine;

namespace RoundLogic
{
    public class RoundRunner : MonoBehaviour
    {
        [SerializeField] private RoundStarter _starter;
        [SerializeField] private RoundEnder _ender;
     
        public RoundStarter Starter => _starter;
        public RoundEnder Ender => _ender;
    }
}
