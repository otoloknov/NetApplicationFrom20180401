using System.Threading;

namespace Attraction
{
    // Class Kid with 7 public attributes + 2 Constructors (1 overloaded) + method ToCry ( if there are no available attractions)
    class Kid
    {
        // Name 
        public string Name { get; }
        // Height
        public int Height { get; private set; }
        // Gender type (M/F)
        public GenderType Gender { get; }
        //Age - not implemented to use.
        public int Age { get; }
        //Satisfaction Level
        private int SatisfactionDegree { get; set; }
        //Money
        private int Money { get; set; }
        //Is enough money for riding on attraction?
        private bool IsEnoughMoney { get; set; }
        // Action name - this the string value for displaying current status of kid. e.g. riding, crying etc...
        private string ActionName { get; set; }
        // Value for understanding if kid free for the next action from Attraction manager or not ?
        private bool IsBusyForNextActions { get; set; }

        // private constants for method toCry.
        private const int INCREASE_HEIGHT_WHEN_CRY = 10;
        private const int DECREASE_SATISFACTION_WHEN_CRY = 15;

        // general Kid constructor with all parameters.
        public Kid(string name, int height,  GenderType gender, int age, int money, int satisfactionDegree):
            this( name,  height, gender,  age,  money)
        {
            SatisfactionDegree = satisfactionDegree;
        }
        // Kid constructor without paramter - satisfactionDegree. By default it equal 100%.
        public Kid(string name, int height, GenderType gender, int age, int money)
        {
            Name = name;
            Height = height;
            Gender = gender;
            Age = age;
            SatisfactionDegree = 100;
            Money = money;
            IsEnoughMoney = true;
            ActionName = "N/A";
            IsBusyForNextActions = false;
        }
        // when kid is crying next conditions should be applied.
        // height +2
        // satisfaction -5
        public void ToCry()
        {
            // Update kid action in correct value based on method call
            SetKidAction("No available Attractions! Will cry now...");
            // Kid is busy not - he/she is crying 
            IsBusyForNextActions = true;
            
            //Imitation of crying in separate thread.
            Thread.Sleep(5000);
            
            //After crying update parameters for a new one.
            Height += INCREASE_HEIGHT_WHEN_CRY;
            SatisfactionDegree -= DECREASE_SATISFACTION_WHEN_CRY;
            
            //update status of activity.
            SetKidAction("Stop crying! New Parameters - H:"+ Height + ",SD:"+SatisfactionDegree);
            // Kid is free 
            IsBusyForNextActions = false;
        }
        // Chanhe Satisfaction Degree on pre-generated random value.
        public void ChangeSatisfactionDegree(int level)
        {
            SatisfactionDegree += level;
        }

        public void ChangeMoneyAmount(int money)
        {
            Money -= money;
        }

        public int GetCurrentMoneyAmount()
        {
            return Money;
        }

        public bool IsEnoughMoneyForAttractions()
        {
            return IsEnoughMoney;
        }

        public void SetNoMoneyFlag(bool isMoney)
        {
            IsEnoughMoney = isMoney;
        }

        public void SetKidAction(string actionaName)
        {
            ActionName = actionaName;
        }
        public string GetKidAction()
        {
            return ActionName;
        }

        public bool GetIsBusyStatus()
        {
            return IsBusyForNextActions;
        }
        public void SetBusyStatus(bool isBusy)
        {
            IsBusyForNextActions = isBusy;
        }

        public int GetSatisfactionDegree()
        {
            return SatisfactionDegree;
        }
    }
}
