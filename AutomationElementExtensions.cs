using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Automation;

namespace AllScriptRipper
{
    static class AutomationElementExtensions
    {
        public static AutomationElement FindByIdPath(this AutomationElement a, string path)
        {
            return FindByPathHelper(a, AutomationElement.AutomationIdProperty, path);
        }

        public static void Invoke(this AutomationElement a)
        {
            ((InvokePattern)a.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
        }

        public static string GetValue(this AutomationElement a)
        {
            return ((ValuePattern)a.GetCurrentPattern(ValuePattern.Pattern)).Current.Value;
        }
        public static bool GetToggleState(this AutomationElement a)
        {
            return ((TogglePattern)a.GetCurrentPattern(TogglePattern.Pattern)).Current.ToggleState == ToggleState.On;
        }

        public static void SetValue(this AutomationElement a, string value)
        {
            ((ValuePattern)a.GetCurrentPattern(ValuePattern.Pattern)).SetValue(value);
        }
        
        public static void Close(this AutomationElement a)
        {
            ((WindowPattern)a.GetCurrentPattern(WindowPattern.Pattern)).Close();
        }

        public static AutomationElement FindByNamePath(this AutomationElement a, string path)
        {
            return FindByPathHelper(a, AutomationElement.NameProperty, path);
        }
        
        public static AutomationElement FindByPathHelper(this AutomationElement a, AutomationProperty p, string path)
        {
            AutomationElement current = a;
            
            foreach (string e in path.Split('/'))
            {
                current = current.FindFirst(TreeScope.Children,
                    new PropertyCondition(p, e)) ??
                          current.FindFirst(TreeScope.Descendants,
                        new PropertyCondition(p, e));
            }

            return current;
        }
        public static void Focus(this AutomationElement element)
        {
            int tries = 8;
            while (tries > 0)
            {
                try
                {
                    element.SetFocus();
                    Thread.Sleep(250);
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(250);
                    tries--;
                }
            }
        }

        public static IList<AutomationElement>GetWindows(this AutomationElement ae, string title = null)
        {
            try
            {
                var results = new List<AutomationElement>();

                Thread.Sleep(100);

                var windows = ae.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.IsWindowPatternAvailableProperty, true));

                foreach (AutomationElement childWindow in windows)
                {
                    if (title == null || childWindow.Current.Name.StartsWith(title))
                    {
                        results.Add(childWindow);

                        foreach(AutomationElement gcWindow in childWindow.GetWindows(title)) // 2nd generation
                        {
                            results.Add(gcWindow);
                        }
                    }
                }

                return results;
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                Thread.Sleep(250);
                return GetWindows(ae, title);
            }
        }
    }
}