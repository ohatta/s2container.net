/*
 * Created by: 
 * Created: 2007�N7��2��
 */

using Seasar.Dxo.Annotation;

namespace Seasar.Tests.Dxo 
{
    public interface IBeanToBeanDxo
    {
        [DatePattern("yyyyMMdd")]
        TargetBean ConvertBeanToTarget(BeanA source);
    }
}