using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Ace.Gengine.Components.System.Collections
{
	public class Collection<T> : ArrayList
		where T : ArrayList
	{
		protected T _ActiveItem;

		public T ActiveItem { get => _ActiveItem; set => _ActiveItem = value; }

		public override int Count => base.Count;

		public override int Add(object value) => base.Add(value);

		public override void AddRange(ICollection c) => base.AddRange(c);

		public override void Clear() => base.Clear();

		public override bool Contains(object item) => base.Contains(item);

		public override IEnumerator GetEnumerator() => base.GetEnumerator();

		public override IEnumerator GetEnumerator(int index, int count) => base.GetEnumerator(index, count);

		public override ArrayList GetRange(int index, int count) => base.GetRange(index, count);

		public override void Remove(object obj) => base.Remove(obj);

		public override void RemoveRange(int index, int count) => base.RemoveRange(index, count);

		public override void SetRange(int index, ICollection c) => base.SetRange(index, c);

		public override object[] ToArray() => base.ToArray();

		public override Array ToArray(Type type) => base.ToArray(type);
	}
}