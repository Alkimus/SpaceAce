using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;

namespace Ace.Gengine.Components.Drawing
{
	/// <summary> A collection for Holding and Handling Sprite Frames </summary>
	public class FrameCollection : Frame
	{
		protected Frame _CurrentFrame;
		protected List<Frame> _Frames;
		protected int _Index;

		public FrameCollection(Rectangle bounds) : base(bounds)
		{
			_Index = 0;
			_Frames = new List<Frame>
			{
				new Frame(_Bounds)
			};
			_CurrentFrame = _Frames[_Index];
		}

		public Frame CurrentFrame { get => _CurrentFrame; set => _CurrentFrame = value is null ? _Frames[_Index] : value; }

		public int Index
		{
			get => _Index;
			set
			{
				_Index = value;
				_CurrentFrame = _Frames[_Index];
			}
		}

		public int GetIndex(Frame frame) => _Frames.FindIndex(x => x == frame);

		public int Count => _Frames.Count;

		public void Add(Frame frame) => _Frames.Add(frame);

		public void AddRange(List<Frame> frames) => _Frames.AddRange(frames);

		public void Remove(Frame frame) => _Frames.Remove(frame);
	}
}