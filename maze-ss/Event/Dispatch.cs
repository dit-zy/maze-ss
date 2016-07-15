using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze_ss.Event
{
    class Dispatch
    {
        public delegate void Handler(Event e);

        private Dictionary<String, List<Handler>> event_handlers;

        public Dispatch()
        {
            event_handlers = new Dictionary<string, List<Handler>>(8);
        }

        public void registerHandler(String eventType, Handler handler)
        {
            List<Handler> handlers;
            if(!event_handlers.TryGetValue(eventType, out handlers))
            {
                handlers = new List<Handler>();
                event_handlers.Add(eventType, handlers);
            }

            handlers.Add(handler);
        }

        public void dispatch(Event e)
        {
            if(!event_handlers.ContainsKey(e.getType()))
            {
                return;
            }

            foreach(Handler handler in event_handlers[e.getType()])
            {
                handler(e);
            }
        }
    }
}
