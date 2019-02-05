using System;

namespace RealmCommander
{
  [Flags]
  public enum Roles
  {
    Knight = 1,
    Commander = 2,
    General = 4,
    Ruler = 8
  }
}