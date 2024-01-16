using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace WinUI3_SelfLearning.Helpers;

/// <summary>
/// 枚举类型到bool类型的转换器，只能用于判断ElementTheme类型的对象。
/// 如果枚举值与预设值一致，则返回True，否则返回False。
/// </summary>
public class EnumToBooleanConverter : IValueConverter
{
    public EnumToBooleanConverter()
    {
    }

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        //如果parameter是string类型的变量，那么把它转换为string类型赋值给enumString。
        //这种写法很常见。
        if (parameter is string enumString)
        {
            //判断这个值在枚举类型中是否存在
            if (!Enum.IsDefined(typeof(ElementTheme), value))
            {
                throw new ArgumentException("ExceptionEnumToBooleanConverterValueMustBeAnEnum");
            }

            var enumValue = Enum.Parse(typeof(ElementTheme), enumString);

            return enumValue.Equals(value);
        }

        throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (parameter is string enumString)
        {
            return Enum.Parse(typeof(ElementTheme), enumString);
        }

        throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");
    }
}
