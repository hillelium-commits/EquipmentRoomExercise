using System;

// <summary>
//Todo: צרו מחלקת חריגים המטפלת במקרים בהם אין מספיק מחשבים זמינים להשאלה.
//שם המחלקה צריך להיות EquipmentUnavailableException.


class EquipmentUnavailableException: Exception
{
    public int RequestedItems { get; }
    public int NeededItems { get; }
    public EquipmentUnavailableException (string message,int requestedItems, int availableItems):base(message)
    {
        RequestedItems = requestedItems;
        NeededItems = requestedItems-availableItems;
    }
}

class EquipmentRoom
{
    private int availableComputers;

    public int AvailableComputers
    {
        get
        {
            return availableComputers;
        }
        set
        {
            // TODO: יש לבדוק שמספר המחשבים אינו שלילי.
            // אם כן, יש לזרוק חריגה מתאימה.
            if (value >= 0)
            {
                availableComputers = value;
            }
            else { throw new ArgumentOutOfRangeException(); }
        }
    }

    public EquipmentRoom(int amount)
    {
        AvailableComputers = amount;
    }

    public void Borrow(int amount)
    {
       
            // TODO: יש לזרוק חריגה מתאימה כאשר אין מספיק מחשבים זמינים.
        

        if (amount > AvailableComputers)
        {
            throw new EquipmentUnavailableException("Oy vey, ein lanu maspik makhsevim", amount, AvailableComputers);
        }

        AvailableComputers -= amount;
        Console.WriteLine($"הושאלו {amount} מחשבים בהצלחה.");
    }
}

class Program
{
    static void Main(string[] args)
    {
            int initialComputers = 10;

            EquipmentRoom room = new EquipmentRoom(initialComputers);

        // יצירת חדר מחשבים עם מספר מחשבים שלילי:
        // לאחר מימוש הבדיקה ב-set, יש לוודא שמצב כזה מטופל כראוי.
        // TODO: יש להוסיף try-catch מתאימים סביב הקריאות שעלולות לזרוק חריגה.
        try
        {
            int negativeComputers = -5;
            EquipmentRoom invalidRoom = new EquipmentRoom(negativeComputers);
        }
        catch (ArgumentOutOfRangeException a)
        {
            Console.WriteLine(a.Message);
        }

        try
        {
            room.Borrow(4);
            Console.WriteLine("ממשיכים בתוכנית...");

            room.Borrow(8);

        }

        catch (EquipmentUnavailableException b)
        {
            Console.WriteLine(b.Message + $" You asked for {b.RequestedItems}, we need {b.NeededItems} more to fullfill the order");
        }
        Console.WriteLine("מספר המחשבים הזמינים: " + room.AvailableComputers);
    }
}
