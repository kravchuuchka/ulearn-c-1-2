using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }

		private Dictionary<char, Action<IVirtualMachine>> dictionary;

		public VirtualMachine(string program, int memorySize)
		{
			Memory = new byte[memorySize];
			Instructions = program;
			var length = program.Length;
			dictionary = new Dictionary<char, Action<IVirtualMachine>>(length);
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			if (!dictionary.ContainsKey(symbol)) 
				dictionary.Add(symbol, execute);
		}

		public void Run()
		{
			for (; InstructionPointer < Instructions.Length; InstructionPointer++)
            {
				if (dictionary.ContainsKey(Instructions[InstructionPointer]))
					dictionary[Instructions[InstructionPointer]](this);
			}
		}
	}
}