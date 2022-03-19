using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Phoenix.Mobile.Core.Utils
{
    public class Grouping<TK, T> : ObservableCollection<T>
    {
        public TK Key { get; private set; }
        private string DisplayName { get; set; }
        private string Icon { get; set; }
        private bool _expanded;

        private bool Expanded
        {
            get => _expanded;
            set
            {
                if (!_expanded.Equals(value))
                {
                    _expanded = value;

                }
            }
        }
        public string StateExpand => Expanded ? "Ẩn" : "Chi tiết";
        public Grouping(TK key, IEnumerable<T> items, string displayName = "", string icon = "")
        {
            Key = key;
            DisplayName = displayName;
            Icon = icon;
            Expanded = false;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
