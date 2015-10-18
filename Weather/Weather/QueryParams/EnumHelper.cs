using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Weather
{
	public static class EnumHelper
	{
		/// <summary>
		/// Convert enum to string - first check for DescriptionAttribute, otherwize - ToString()
		/// </summary>
		/// <param name="en">Enum value</param>
		public static string GetDescription(this Enum en)
		{
			Type type = en.GetType();

			MemberInfo[] memInfo = type.GetMember(en.ToString());

			if (memInfo != null && memInfo.Length > 0)
			{

				object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute),
					false);

				if (attrs != null && attrs.Length > 0)

					return ((DescriptionAttribute)attrs[0]).Description;
			}

			return en.ToString();
		}

		/// <summary>Return an enum's Display Name.  If there is none, return ToString()</summary>
		public static string GetDisplayValue(this Enum en)
		{
			var fieldInfo = en.GetType().GetField(en.ToString());

			var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

			if (descriptionAttributes == null) return string.Empty;
			return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : en.ToString();
		}

		/// <summary>
		/// Check to see if a flags enumeration has a specific flag set.
		/// </summary>
		/// <param name="variable">Flags enumeration to check</param>
		/// <param name="value">Flag to check for</param>
		/// <returns></returns>
		/// TODO remove this method when we will move to .Net 4.5
		public static bool HasFlag(this Enum variable, Enum value)
		{
			if (variable == null)
				return false;

			if (value == null)
				throw new ArgumentNullException("value");

			if (!Enum.IsDefined(variable.GetType(), value))
			{
				throw new ArgumentException(string.Format(
					"Enumeration type mismatch.  The flag is of type '{0}', was expecting '{1}'.",
					value.GetType(), variable.GetType()));
			}

			ulong num = Convert.ToUInt64(value);
			return ((Convert.ToUInt64(variable) & num) == num);
		}

		public static T SetFlag<T>(this Enum flags,
			Enum flag, bool value)
		{
			if (!Enum.IsDefined(flags.GetType(), flag))
			{
				throw new ArgumentException(string.Format(
					"Enumeration type mismatch.  The flag is of type '{0}', was expecting '{1}'.",
					flag.GetType(), flags.GetType()));
			}
			var uflags = Convert.ToUInt64(flags);
			if (value)
			{
				uflags |= Convert.ToUInt64(flag);
			}
			else
			{
				uflags &= ~Convert.ToUInt64(flag);
			}
			return (T)Enum.Parse(flags.GetType(), uflags.ToString());
		}

		/// <summary>Get the value T of a custom attribute R on enum @enum. Usage ex:  EnumHelper.GetAttributeValue&lt;TrialOrPaid, TrialOrPaidAttribute&gt;(AccountType.PaidBusiness) returns TrialOrPaid.Paid</summary>
		/// <typeparam name="T">Type of the values of the custom attribute, for ex: the enum TrialOrPaid</typeparam>
		/// <typeparam name="R">The custom attribute class name, for ex: TrialOrPaidAttribute</typeparam>
		/// <param name="enum">An instance of the enum with custom attribute R, for ex:AccountType.PaidBusiness</param>
		/// <returns>Value of attribute R for this instance of T, for ex: TrialOrPaid.Paid</returns> 
		public static T GetAttributeValue<T, R>(IConvertible @enum)
		{
			T attributeValue = default(T);

			if (@enum != null)
			{
				FieldInfo fi = @enum.GetType().GetField(@enum.ToString());

				if (fi != null)
				{
					R[] attributes = fi.GetCustomAttributes(typeof(R), false) as R[];

					if (attributes != null && attributes.Length > 0)
					{
						IAttribute<T> attribute = attributes[0] as IAttribute<T>;

						if (attribute != null)
						{
							attributeValue = attribute.Value;
						}
					}
				}
			}

			return attributeValue;
		}
	}

	/// <summary>Used for retrieving custome attributes from enums</summary>
	public interface IAttribute<T>
	{
		T Value { get; }
	}
}

