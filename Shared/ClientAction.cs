using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared {
  [Serializable]
  public class ClientAction {
    public ActionType Action { get; set; }
    public string Message { get; set; } 
  }

  public enum ActionType {
    None,
    Stop
  }
}
