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

        public static void SetExpandingBehaviour(DependencyObject dependencyObj, ICommand value)
        {
            dependencyObj.SetValue(ExpandingBehaviourProperty, value);
        }

        public static ICommand GetExpandingBehaviour(DependencyObject dependencyObj)
        {
            return (ICommand)dependencyObj.GetValue(ExpandingBehaviourProperty);
        }

        private static void OnExpandingBehaviourChanged(DependencyObject dependencyObj, DependencyPropertyChangedEventArgs dependencyPropertyEvent)
        {
            if (!(dependencyObj is TreeViewItem treeViewItem)) return;

            if (dependencyPropertyEvent.NewValue is ICommand command)
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
