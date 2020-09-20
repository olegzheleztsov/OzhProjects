using System.Windows;
using System.Windows.Media;

namespace NuclearWar.Controls
{
    public class UIHelper
    {
        public T FindObjectInParent<T>(object currentObject) where T : DependencyObject
        {
            if(currentObject == null)
            {
                return null;
            }

            if(currentObject is T)
            {
                return currentObject as T;
            }
            else
            {
                return FindObjectInParent<T>(VisualTreeHelper.GetParent(currentObject as DependencyObject));
            }
        }

        public (bool success, T currentTarget) CaptureMouseInParent<T>(DependencyObject currentElement) where T : UIElement
        {
            if(currentElement == null)
            {
                return (false, default);
            }

            if(currentElement is T)
            {
                (currentElement as T).CaptureMouse();
                return (true, currentElement as T);
            } else
            {
                return CaptureMouseInParent<T>(VisualTreeHelper.GetParent(currentElement));
            }
        } 
        
        public (bool success, T captureTarget) ReleaseMouseInParent<T>(DependencyObject currentElement) where T : UIElement
        {
            if (currentElement == null)
            {
                return (false, default);
            }

            if (currentElement is T)
            {
                (currentElement as T).ReleaseMouseCapture();
                return (true, currentElement as T);
            }
            else
            {
                return ReleaseMouseInParent<T>(VisualTreeHelper.GetParent(currentElement));
            }
        }
    }
}
