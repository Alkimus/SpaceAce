using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

using System.Collections.Generic;

namespace Ace.Gengine.Input
{
	  public class TouchInputCollection
	  {
		    protected Dictionary<string, Touch> _Zones = new Dictionary<string, Touch>();
		    protected bool TouchAble;

		    public TouchInputCollection()
				=> TouchAble = TouchPanel.GetCapabilities().IsConnected;

		    public void AddArea(string name, Touch zone)
			  => _Zones.Add(name, zone);

		    public void RemoveArea(string name)
			  => _Zones.Remove(name);

		    public bool IsActive(string name)
			  => _Zones[name].IsTouched;

		    public void Clear()
				=> _Zones.Clear();

		    public List<Touch> InputZones()
		    {
				List<Touch> DebugZones = new List<Touch>();
				foreach (var item in _Zones.Values)
				{
					  DebugZones.Add(item);
				}
				return DebugZones;
		    }

		    public List<Vector2> TouchLocations()
		    {
				TouchCollection touchLocations = TouchPanel.GetState();

				List<Vector2> list = new List<Vector2>();

				foreach (var item in touchLocations)
					  list.Add(new Vector2(item.Position.X, item.Position.Y));

				return list;
		    }

		    public void Update()
		    {
				foreach (var loc in TouchLocations())
				{
					  foreach (var area in _Zones.Values)
					  {
						    area.Is_Touched(loc);
						    if (area.IsTouched) area.Invoke_Action();
					  }
				}
		    }
	  }
}