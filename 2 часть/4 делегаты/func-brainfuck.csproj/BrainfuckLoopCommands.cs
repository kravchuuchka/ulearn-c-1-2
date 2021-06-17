using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		private static Stack<int> stack = new Stack<int>();
		private static Dictionary<int, int> dictionary = new Dictionary<int, int>();

		private static void Find(IVirtualMachine b)
		{
			for (int i = 0; i < b.Instructions.Length; i++)
			{
				if (b.Instructions[i] == '[') stack.Push(i);
				if (b.Instructions[i] == ']')
				{
					var index = stack.Pop(); 
					dictionary[index] = i;
					dictionary[i] = index;
				}
			}
		}

		public static void RegisterTo(IVirtualMachine vm)
		{
			Find(vm);
			vm.RegisterCommand('[', b => {
				if (b.Memory[b.MemoryPointer] == 0)
					b.InstructionPointer = dictionary[b.InstructionPointer];
			});
			vm.RegisterCommand(']', b => {
				if (b.Memory[b.MemoryPointer] != 0)
					b.InstructionPointer = dictionary[b.InstructionPointer];
			});
		}
	}
}