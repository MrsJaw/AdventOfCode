﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day7
{
    class Program
    {
        #region Instruction
        public struct Instruction
        {
            public CircuitOperation Operation;
            public string[] Operands;
            public string Recipient;
            public string OriginalLine;
        }
        #endregion Instruction

        #region CircuitOperation
        public enum CircuitOperation
        {
            Default,
            And,
            Or,
            LShift,
            RShift,
            Not
        }
        #endregion CircuitOperation

        #region Member Variables
        public static SortedDictionary<string, UInt16> _Circuit = new SortedDictionary<string, UInt16>();
        #endregion Member Variables

        #region Main
        static void Main(string[] args)
        {
            string FilePath = string.Empty;

            //Get Input
            Console.Write("Merry Christmas Bobby! What's the file path of your directions?  ");
            FilePath = Console.ReadLine();

            //Construct Circuit
            try
            {
                List<Instruction> CircuitInstructions = ConstructCircuit(FilePath);                
                LetErRip(CircuitInstructions);
                WriteResults();

                Console.WriteLine("Let's go another round!");
                UInt16 aValue = _Circuit["a"];
                _Circuit = new SortedDictionary<string, UInt16>();
                Dictionary<string, Instruction> InstructionsByRecipient = CircuitInstructions.ToDictionary(i => i.Recipient);
                InstructionsByRecipient["b"] = CreateCircuitInstruction($"{aValue} -> b");
                LetErRip(InstructionsByRecipient.Values.ToList());
                WriteResults();
            }
            catch (System.Exception ex)
            {                
                Console.WriteLine("Sorry Bobby, you've got bad directions."); 
                Console.WriteLine(ex.Message);               
            }

            Console.WriteLine("Press any key to exit.");
            Console.Read();
        }
        #endregion Main

        #region ConstructCircuit
        private static List<Instruction> ConstructCircuit(string filePath)
        {
            List<Instruction> UncompletedInstructions = new List<Instruction>();

            //Compile Instructions
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string Line = reader.ReadLine();
                    Instruction LineInstruction = CreateCircuitInstruction(Line);
                    UncompletedInstructions.Add(LineInstruction);
                }
            }
            
            return UncompletedInstructions;
        }
        #endregion ConstructCircuit

        #region CreateCircuitInstruction
        private static Instruction CreateCircuitInstruction(string line)
        {
            string[] LineInstructions = SplitString(line, @"[->]");
            Instruction Result = new Program.Instruction();

            if (LineInstructions[0].Contains("AND"))
            {
                Result = new Program.Instruction(){ Operation = CircuitOperation.And, Operands = SplitString(LineInstructions[0], "[AND]")};
            }
            else if (LineInstructions[0].Contains("OR"))
            {
                Result = new Program.Instruction(){ Operation = CircuitOperation.Or, Operands = SplitString(LineInstructions[0], "[OR]") };
            }
            else if (LineInstructions[0].Contains("LSHIFT"))
            {
                Result = new Program.Instruction(){ Operation = CircuitOperation.LShift, Operands = SplitString(LineInstructions[0], "[LSHIFT]") };
            }
            else if (LineInstructions[0].Contains("RSHIFT"))
            {
                Result = new Program.Instruction(){ Operation = CircuitOperation.RShift, Operands = SplitString(LineInstructions[0], "[RSHIFT]") };
            }
            else if (LineInstructions[0].Contains("NOT"))
            {
                Result = new Program.Instruction(){ Operation = CircuitOperation.Not, Operands = new string[] { LineInstructions[0].Replace("NOT ", "") } };
            }
            else 
            {
                Result = new Program.Instruction() { Operation = CircuitOperation.Default, Operands = new string[] { LineInstructions[0] } };
            }

            Result.Recipient = LineInstructions.LastOrDefault();
            Result.OriginalLine = line;

            return Result;
        }
        #endregion CreateCircuitInstruction

        #region LetErRip
        public static void LetErRip(List<Instruction> instructions)
        {
            List<Instruction> InstructionsToComplete = instructions.Select(i => new Instruction(){Operation = i.Operation,
                                                                                                  Operands = i.Operands.Select(o => o).ToArray(),
                                                                                                  Recipient = i.Recipient,
                                                                                                  OriginalLine = i.OriginalLine}).ToList();
            //Perform Instructions
            while(InstructionsToComplete.Count > 0)
            {
                for(int i = 0; i < InstructionsToComplete.Count; i ++)
                {
                    Instruction NextInstruction = InstructionsToComplete[i];
                    if(CompleteCircuitInstruction(NextInstruction))
                    {
                        InstructionsToComplete.Remove(NextInstruction);
                        i--;
                    }
                }
            }
        }
        #endregion LetErRip

        #region CompleteCircuitInstruction
        public static bool CompleteCircuitInstruction(Instruction instruction)
        {
            bool InstructionCanBeCompleted = true;
            UInt16 Value = 0;
            UInt16 Operand1 = 0;
            UInt16 Operand2 = 0;

            if(_Circuit.ContainsKey(instruction.Operands[0]))
            {
                Operand1 = _Circuit[instruction.Operands[0]];
            }
            else if (!UInt16.TryParse(instruction.Operands[0], out Operand1))
            {
                InstructionCanBeCompleted = false;
            }

            if (instruction.Operands.Length > 1)
            {
                if (_Circuit.ContainsKey(instruction.Operands[1]))
                {
                    Operand2 = _Circuit[instruction.Operands[1]];
                }
                else if (!UInt16.TryParse(instruction.Operands[1], out Operand2))
                {
                    InstructionCanBeCompleted = false;
                }
            }

            if (InstructionCanBeCompleted)
            {
                switch (instruction.Operation)
                {
                    case (CircuitOperation.And):
                        Value = (UInt16)(Operand1 & Operand2);
                        break;
                    case (CircuitOperation.Or):
                        Value = (UInt16)(Operand1 | Operand2);
                        break;
                    case (CircuitOperation.LShift):
                        Value = (UInt16)(Operand1 << Operand2);
                        break;
                    case (CircuitOperation.RShift):
                        Value = (UInt16)(Operand1 >> Operand2);
                        break;
                    case (CircuitOperation.Not):
                        Value = (UInt16)(~Operand1);
                        break;
                    default:
                        Value = Operand1;
                        break;
                }

                _Circuit[instruction.Recipient] = Value;
                
            }
            
            return InstructionCanBeCompleted;

        }
        #endregion CompleteCircuitInstruction

        #region SplitString
        private static string[] SplitString(string input, string pattern)
        {
            return Regex.Split(input, pattern).Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).ToArray();
        }
        #endregion SplitString

        #region WriteResults
        public static void WriteResults()
        {
            Console.WriteLine("Wire Signals");
            Console.WriteLine("-----------------");
            foreach (string wire in _Circuit.Keys)
            {
                Console.WriteLine(wire + ": " + _Circuit[wire].ToString());
            }    
        }
        #endregion WriteResults
    }
}

