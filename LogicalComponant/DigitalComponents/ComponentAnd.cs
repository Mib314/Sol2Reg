namespace Sol2Reg.LogicalComponent.DigitalComponents
{
	using System;
	using System.Collections.Generic;
	using ComponentBase;
	using DataObject;
	using Interface;
	using Interface.ComponentBase;

	public class ComponentAnd : BasicComponent
	{
		private readonly List<MappInputNameToOutput> inputOutputNames;

		public ComponentAnd(IParametersManager pParametersManager)
			:base(pParametersManager)
		{
			this.inputOutputNames = new List<MappInputNameToOutput>();
		}
		private bool Output { get; set; }

		public ComponentAnd Initilize(string code)
		{
			this.Code = code;
			return this;
		}
		private void Calculate()
		{
			this.CheckIfParamsIsValid();

			if (parma1IsOk && param2IsOk)
			{
				Console.WriteLine(string.Format("Calculate And  : code {1}, cycle {0} => Out : {2}", this.Cycle, this.Code, this.Output));
				Output = InputValue1 && InputValue2;
				OnEventOutputChange(Output);
				parma1IsOk = param2IsOk = false;
			}
		}

		public void RegisterLinkInput1(BasicComponent outputComponent)
		{
			outputComponent.EventOutputChange += (o, args) =>
			                       {
			                       	this.InputValue1 = args.DigitalValue ?? false;
			                       	parma1IsOk = true;
									this.Cycle = args.Cycle;
									this.Calculate();
			                       };
		}

		public void RegisterLinkInput2(BasicComponent outputComponent)
		{
			outputComponent.EventOutputChange += (o, args) =>
			                       {
			                       	this.InputValue2 = args.DigitalValue ?? false;
			                       	param2IsOk = true;
									this.Cycle = args.Cycle;
									this.Calculate();
			                       };
		}

	}
}