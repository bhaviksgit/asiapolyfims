using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PolyFilms.Common
{
    public class Enums
    {
        public enum NotifyType
        {

            /// <summary>
            /// Success Enum Value setting.
            /// </summary>
            [Display(Name = "Success")]
            [Description("Success")]
            Success = 1,

            /// <summary>
            /// Error Enum Value setting.
            /// </summary>
            [Display(Name = "Error")]
            [Description("Error")]
            Error = 0
        }

        public enum JumboStatus
        {
            Q0 = 7,
            Q1 = 8,
            Q2 = 9 ,
            Q4 = 10 ,
            NEW = 11 ,
            ReadyForTest = 12
        }

        public enum SlitingStatus
        {
            Q0 = 6,
            Q1 = 7,
            Q2 = 8,
            Q3 = 9,
            Q4 = 10,
            SC = 11
        }

        public enum OrderStatus
        {
            Pending = 1,
            PartiallyDispatched = 2,
            Dispatched = 3,
            Invoiced = 4,
            Cancelled = 5

        }

        public enum ReceiptType
        {
            To,
            Cc,
            Bcc,
            Attachment
        }
    }

    public static class EnumExtension
    {
        /// <summary>
        /// The get description.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetDescription(this Enum element)
        {
            var type = element.GetType();
            var memberInfo = type.GetMember(Convert.ToString(element));
            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            return Convert.ToString(element);
        }
    }
    }
