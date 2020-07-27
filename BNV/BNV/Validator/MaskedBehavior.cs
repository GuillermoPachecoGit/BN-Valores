using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace BNV.Validator
{
    public class MaskedBehavior : Behavior<Entry>
    {
        private string _mask = "";
        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
                SetPositions();
            }
        }

        private int _minSize;
        public int MinSize
        {
            get => _minSize;
            set
            {
                _minSize = value;
            }
        }

        private int _maxSize;
        public int MaxSize
        {
            get => _maxSize;
            set
            {
                _maxSize = value;
            }
        }

        private bool _isAlphanumeric;
        public bool IsAlphanumeric
        {
            get => _isAlphanumeric;
            set
            {
                _isAlphanumeric = value;
            }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        IDictionary<int, char> _positions;

        void SetPositions()
        {
            if (string.IsNullOrEmpty(Mask))
            {
                _positions = null;
                return;
            }

            var list = new Dictionary<int, char>();
            for (var i = 0; i < Mask.Length; i++)
                if (Mask[i] != '#')
                    list.Add(i, Mask[i]);

            _positions = list;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;

            var text = entry.Text;

            if (string.IsNullOrWhiteSpace(text) || _positions == null)
                return;

            if (!IsAlphanumeric && text.Replace("-", String.Empty).ToCharArray().Any(x => !char.IsDigit(x) && (x != '/')))
             {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            if (text.Length > MaxSize)
            {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            foreach (var position in _positions)
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();
                    if (text.Substring(position.Key, 1) != value)
                        text = text.Insert(position.Key, value);
                }

            if (entry.Text != text)
                entry.Text = text;
        }
    }
}
