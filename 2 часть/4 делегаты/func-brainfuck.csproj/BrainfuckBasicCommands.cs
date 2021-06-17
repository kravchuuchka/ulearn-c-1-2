using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b => { write(Convert.ToChar(b.Memory[b.MemoryPointer])); });
			vm.RegisterCommand('+', b => 
			{
				GoAnotherByte(b, 1, 256, b.Memory[b.MemoryPointer]);
			});
			vm.RegisterCommand('-', b => 
			{
				GoAnotherByte(b, -1, 256, b.Memory[b.MemoryPointer]);
			});
			vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = Convert.ToByte(read()); });
			vm.RegisterCommand('>', b =>
			{
				//if (b.MemoryPointer == b.Memory.Length - 1) b.MemoryPointer = 0;
				//else b.MemoryPointer++;
				GoAnotherByte(b, 1, b.Memory.Length - 1, b.MemoryPointer);
			});
			vm.RegisterCommand('<', b =>
			{
				//if (b.MemoryPointer == 0) b.MemoryPointer = b.Memory.Length - 1;
				//else b.MemoryPointer--;
				GoAnotherByte(b, -1, b.Memory.Length - 1, b.MemoryPointer);
			});
			RegisterSymbols(vm);
			
		}

        private static int GoAnotherByte(IVirtualMachine b, int number, int length, int smth)
        {
            //return b.Memory[b.MemoryPointer] = (byte)((b.Memory[b.MemoryPointer] + number + length) % length);
			return smth = (byte)((smth + number + length) % length);
		}

		/*private static int DoSomething(IVirtualMachine b, int number, int length)
        {
			return b.MemoryPointer = (byte)(b.MemoryPointer + number + length) % length;
        }*/

        public static void RegisterSymbols(IVirtualMachine vm)
        {
			char[] symbols = new char[62];
			AddSymbolsToArray('A', 'Z', symbols, 0);
			AddSymbolsToArray('a', 'z', symbols, 26);
			AddSymbolsToArray('0', '9', symbols, 52);

			foreach (var e in symbols)
				vm.RegisterCommand(e, b => { b.Memory[b.MemoryPointer] = Convert.ToByte(e); });
		}

		private static void AddSymbolsToArray(char firstValue, char lastValue, char[] symbols, int counter)
        {
			for (var i = firstValue; i <= lastValue; i++)
			{
				symbols[counter] = i;
				counter++;
			}
		}
	}
}