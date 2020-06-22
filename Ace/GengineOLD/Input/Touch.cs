using Microsoft.Xna.Framework;

using System;

namespace Ace.Gengine.Input
{
	  public class Touch
	  {
		    protected Rectangle _Zone;
		    protected bool _Touched;
		    protected string _Name;
		    protected Action _Command;

		    public Touch(string name, Rectangle area, Action command)
		    {
				_Name = name;
				_Zone = area;
				_Command = command;
		    }

		    public Action Get_Action => _Command;

		    public Rectangle Get_TouchZone() => _Zone;

		    public void Set_Action(Action value) => _Command = value;

		    public void Set_TouchZone(Rectangle value) => _Zone = value;

		    public void Is_Touched(Vector2 location) => _Touched = _Zone.Contains(location.X, location.Y) ? true : false;

		    public void Not_Touched(Vector2 location) => _Touched = _Zone.Contains(location.X, location.Y) ? true : false;

		    public void Invoke_Action() => _Command.Invoke();
	  }
}