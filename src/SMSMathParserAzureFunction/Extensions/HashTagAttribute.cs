using System;

namespace SMSMathParserAzureFunction.Extensions
{
	public class HashTagAttribute : Attribute
	{
		public HashTagAttribute(string value)
		{
			Value = value;
		}

		public string Value { get; }
	}
}
