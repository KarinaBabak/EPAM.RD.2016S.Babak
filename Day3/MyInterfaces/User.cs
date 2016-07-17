using System;

namespace MyInterfaces
{
    public class User : MarshalByRefObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
