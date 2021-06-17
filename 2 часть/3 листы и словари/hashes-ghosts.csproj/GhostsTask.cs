using System;
using System.Text;

namespace hashes
{
	public class GhostsTask : 
		IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, 
		IMagic
	{
        byte[] content;
        Document document;
        Vector vector = new Vector(0, 0);
        Segment segment = new Segment(new Vector(1, 2), new Vector(3, 4));
        Cat cat = new Cat("Barsik", "Maine coon", DateTime.Now);
        Robot robot = new Robot("Ar-2000");

        Document IFactory<Document>.Create() => document;

        Vector IFactory<Vector>.Create() => vector;

        Segment IFactory<Segment>.Create() => segment;

        Cat IFactory<Cat>.Create() => cat;

        Robot IFactory<Robot>.Create() => robot;

        public GhostsTask()
        { 
            var encoding = Encoding.ASCII;
            content = encoding.GetBytes("Hello Maksim Emelyanov");
            document = new Document("Programming", encoding, content);
        }

        public void DoMagic()
		{
			for (var i = 0; i < content.Length; i++)
				++content[i];
			vector.Add(new Vector(6, 7));
			segment.End.Add(new Vector(8, 9));
			cat.Rename("Kuzya");
			Robot.BatteryCapacity /= 2;
		}
	}
}