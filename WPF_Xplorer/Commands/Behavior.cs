using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_Xplorer.Commands
{
    public static class Behaviour
    {
        public static readonly DependencyProperty ExpandingBehaviourProperty =
            DependencyProperty.RegisterAttached("ExpandingBehaviour", typeof(ICommand), typeof(Behaviour),
                new PropertyMetadata(OnExpandingBehaviourChanged));

        public static void SetExpandingBehaviour(DependencyObject o, ICommand value)
        {
            o.SetValue(ExpandingBehaviourProperty, value);
        }

        public static ICommand GetExpandingBehaviour(DependencyObject o)
        {
            return (ICommand)o.GetValue(ExpandingBehaviourProperty);
        }

        private static void OnExpandingBehaviourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TreeViewItem treeViewItem)) return;

            if (e.NewValue is ICommand command)
            {
                treeViewItem.Expanded += (obj, routedEventArgs) =>
                {
                    if (command.CanExecute(routedEventArgs))
                    {
                        command.Execute(routedEventArgs);

                    }
                    routedEventArgs.Handled = true;
                };
            }
        }
    }
}
