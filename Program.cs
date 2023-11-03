using System.Diagnostics;
using System.Media;

decimal total = 0.0M;
decimal trueTotal = 0.0M;
decimal changeNeeded = 0.0M;
decimal registerTotal = 0.0M;
decimal[,] registerArray = new decimal[2, 10] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
string cashOrCard;
string creditCardNumber = "";
char cashBack = 'C';
int cclength = 0;
char[] ccArray = new char[] { };
string cardType = "No_Card_Used";
decimal userMoney = 0.0M;
Random rdm = new Random();
string userInput;

int transactNum = rdm.Next(1000, 100000);
string transNumString = transactNum.ToString();

DateTime now = DateTime.Now;
string dateNow = now.ToString("MMM-dd-yy");
string timeNow = now.ToString("T");
Console.WriteLine(dateNow);
Console.WriteLine("Hello! Welcome to the NHS Self-Checkout");
Console.WriteLine("Press any key to continue");
Console.ReadKey();

//call get user total that gets the total of the items and return the total of all items.
trueTotal = getUserTotal(total);
total = trueTotal;

Console.WriteLine("Do you want to pay with Cash or Credit Card? (Type 'CA' for cash or 'CC' for credit card.)");
cashOrCard = Console.ReadLine();//establishes cash or card payment 
cashOrCard = cashOrCard.ToUpper();

while (cashOrCard != "CA" && cashOrCard != "CC")
{
    Console.WriteLine("Please enter a valid response. Do you want to pay with Cash or Credit Card? (Type 'CA' for cash or 'CC' for credit card.)");
    cashOrCard = Console.ReadLine();//establishes cash or card payment
    cashOrCard = cashOrCard.ToUpper();
}





if (cashOrCard == "CA")//if cash payment
{

    registerArray = GetRegister(registerArray);

    //call get user money to get the amount of change they need;
    userMoney = GetUserMoney(total, changeNeeded, registerArray);
    changeNeeded = (userMoney - total);
    total -= userMoney;
    //display change needed
    Console.WriteLine("Thank you! Your change is: $" + changeNeeded);



    greedyAlgo(changeNeeded, registerTotal, registerArray);//calls greedy algo to give change if paid with cash.

}



if (cashOrCard == "CC")//credit card payment
{
    cashBack = CashBackFunc(cashBack);
    if (cashBack == 'y')//if they do want cash back
    {
        Console.WriteLine("How much cash back would you like?");
        userInput = Console.ReadLine();
        while (string.IsNullOrEmpty(userInput))
        {
            Console.WriteLine("Do you actually want cashback or not? Enter how much cash you'd like.");
            userInput = Console.ReadLine();
        }
        //idk how else to tell the code not accept any input that contains a letter
        while (userInput.Contains('a') || userInput.Contains('b') || userInput.Contains('c') || userInput.Contains('d') ||
            userInput.Contains('e') || userInput.Contains('f') || userInput.Contains('g') || userInput.Contains('h') ||
            userInput.Contains('i') || userInput.Contains('j') || userInput.Contains('k') || userInput.Contains('m') ||
            userInput.Contains('o') || userInput.Contains('p') || userInput.Contains('q') || userInput.Contains('n') ||
            userInput.Contains('r') || userInput.Contains('s') || userInput.Contains('t') || userInput.Contains('u') ||
            userInput.Contains('v') || userInput.Contains('w') || userInput.Contains('x') || userInput.Contains('z') || userInput.Contains('y') || userInput.Contains(' '))
        {
            Console.WriteLine("Please enter how much cash you'd like back.");
            userInput = Console.ReadLine();
            while (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Do you actually want cashback or not? Enter how much cash you'd like.");
                userInput = Console.ReadLine();
            }
        }
        decimal cashBackAmmount = decimal.Parse(userInput);
        total = total + cashBackAmmount;

        Console.WriteLine("Your total with cashback is: $" + total);
        while (total > 0)
        {
            creditCardNumber = GetCreditCardNumber(creditCardNumber);
            cardType = whatTypeOfCard(creditCardNumber, cardType);
            Console.WriteLine("You are using: " + cardType);
            if (ValidCardNumber(creditCardNumber, cclength) == true)
            {
                Console.WriteLine("Valid card. One moment...");
            }
            else if (ValidCardNumber(creditCardNumber, cclength) == false)
            {
                Console.WriteLine("Card not valid");
                while (ValidCardNumber(creditCardNumber, cclength) == false)
                {
                    Console.WriteLine("Please enter a new card number");
                    creditCardNumber = GetCreditCardNumber(creditCardNumber);
                    if (ValidCardNumber(creditCardNumber, cclength) == true)
                    {
                        Console.WriteLine("Card valid");
                    }
                }
            }

            string[] moneyRequest = MoneyRequest(creditCardNumber, total);
            Console.WriteLine(moneyRequest[0]);
            Console.WriteLine(moneyRequest[1]);
            if (moneyRequest[1] == "declined")
            {
                Console.WriteLine("Your remaining balance is $" + total + " please try another card.");


            }
            else if (Convert.ToDecimal(moneyRequest[1]) < (total))
            {
                total -= Convert.ToDecimal(moneyRequest[1]);
                Console.WriteLine("Your remaining balance is $" + total + " please try another card.");

            }
            else if (Convert.ToDecimal(moneyRequest[1]) == (total))
            {
                total -= Convert.ToDecimal(moneyRequest[1]);
            }
        }
        registerArray = GetRegister(registerArray);
        greedyAlgo(cashBackAmmount, registerTotal, registerArray);
    }


    if (cashBack == 'n')//if they do not want cash back.
    {

        while (total > 0)
        {

            if (cashOrCard == "CA")//if cash payment
            {
                registerArray = GetRegister(registerArray);

                //call get user money to get the amount of change they need;
                userMoney = GetUserMoney(total, changeNeeded, registerArray);
                changeNeeded = (userMoney - total);
                total -= userMoney;

                //display change needed
                Console.WriteLine("Thank you! Your change is: $" + changeNeeded);

                greedyAlgo(changeNeeded, registerTotal, registerArray);//calls greedy algo to give change if paid with cash.

            }
            else if (cashOrCard == "CC")
            {


                creditCardNumber = GetCreditCardNumber(creditCardNumber);
                cardType = whatTypeOfCard(creditCardNumber, cardType);
                Console.WriteLine(cardType);


                if (ValidCardNumber(creditCardNumber, cclength) == true)
                {
                    Console.WriteLine("Card valid");
                }
                else if (ValidCardNumber(creditCardNumber, cclength) == false)
                {
                    Console.WriteLine("Card not valid");
                    while (ValidCardNumber(creditCardNumber, cclength) == false)
                    {
                        Console.WriteLine("Please enter a new card number");
                        creditCardNumber = GetCreditCardNumber(creditCardNumber);
                        if (ValidCardNumber(creditCardNumber, cclength) == true)
                        {
                            Console.WriteLine("Card valid");
                        }
                    }
                }

                string[] moneyRequest = MoneyRequest(creditCardNumber, total);
                Console.WriteLine(moneyRequest[0]);
                Console.WriteLine(moneyRequest[1]);
                if (moneyRequest[1] == "declined")
                {
                    Console.WriteLine("Your remaining balance is $" + total + " would you like to pay the rest with cash or card? CA/CC");
                    cashOrCard = Console.ReadLine();

                }
                else if (Convert.ToDecimal(moneyRequest[1]) < (total))
                {
                    total -= Convert.ToDecimal(moneyRequest[1]);
                    Console.WriteLine("Your remaining balance is $" + total + " would you like to pay the rest with cash or card? CA/CC");
                    cashOrCard = Console.ReadLine();
                }
                else if (Convert.ToDecimal(moneyRequest[1]) == (total))
                {
                    total -= Convert.ToDecimal(moneyRequest[1]);
                }
            }
        }
    }
}



Console.WriteLine("Thank you! Purchase is complete! Have a lovely day!");

string userMoneyStr = userMoney.ToString();
string trueTotalStr = trueTotal.ToString();
string changeNeededStr = changeNeeded.ToString();

string[] arg_array = new string[7]
{
    transNumString, dateNow, timeNow, userMoneyStr, cardType, trueTotalStr, changeNeededStr
};

ProcessStartInfo startInfo = new ProcessStartInfo();
startInfo.FileName = "C:\\Users\\MCA 008\\source\\repos\\Kiosk Transaction Log\\Kiosk Transaction Log\\bin\\Debug\\net6.0\\Kiosk Transaction Log.exe";
startInfo.Arguments = arg_array[0] + " " + arg_array[1] + " " + arg_array[2] + " " + arg_array[3] + " " + arg_array[4] + " " + arg_array[5] + " " + arg_array[6];
Process.Start(startInfo.FileName, startInfo.Arguments);



Console.ReadKey();
//end main

//______________________________________________________________________________________________________________________________________

//function to get the sale total
static char CashBackFunc(char cashBack)
{
    Console.WriteLine("Would you like Cash Back? (y/n)");
    string userInput = Console.ReadLine();
    while (userInput.Length != 1)
    {
        Console.WriteLine("Please enter a valid response. Would you like Cash Back? (y/n)");
        userInput = Console.ReadLine();
    }
    while (userInput != "y" && userInput != "n")//if they dont anser yes or no
    {
        Console.WriteLine("Please enter a valid response. Would you like Cash Back? (y/n)");
        userInput = Console.ReadLine();
    }
    cashBack = char.Parse(userInput);
    return cashBack;
}

static decimal getUserTotal(decimal total)
{
    bool anotherItem = true;
    string userInput;

    while (anotherItem)
    {
        Console.WriteLine("Enter the price of your item: ");

        userInput = Console.ReadLine();
        if(userInput == "banana" || userInput == "Banana" || userInput == "BANANA")
        {
            SoundPlayer player = new SoundPlayer("you-didn't-say-the-magic-word.wav");
            player.Play();
        }

        int checker = 0;

        do
        {
            checker = 0;
            if (userInput.Contains('.'))
            {

                for (int i = 0; i < userInput.Length; i++)
                {
                    if (checker >= 1 || userInput[i] == '.')
                    {
                        checker++;
                    }
                }
            }

            if (!decimal.TryParse(userInput, out _) || decimal.Parse(userInput) < 0 || checker > 3)
            {
                Console.WriteLine("Invalid Input. Please enter the price of your item:");
                userInput = Console.ReadLine();
                if (userInput == "banana" || userInput == "Banana" || userInput == "BANANA")
                {
                    SoundPlayer player = new SoundPlayer("you-didn't-say-the-magic-word.wav");
                    player.Play();
                }
                checker = 0;
                if (userInput.Contains('.'))
                {

                    for (int i = 0; i < userInput.Length; i++)
                    {
                        if (checker >= 1 || userInput[i] == '.')
                        {
                            checker++;
                        }
                    }
                }
            }

        } while (!decimal.TryParse(userInput, out _) || decimal.Parse(userInput) < 0 || checker > 3);



        total += decimal.Parse(userInput);
        Console.Clear();
        Console.WriteLine("\t\t\t\t\t\t\t\t\t\t Running total: \t\t$" + total);
        Console.WriteLine("Do you have another item? (y/n)");
        userInput = Console.ReadLine();
        while (userInput.Length != 1)
        {
            Console.WriteLine("Please enter y/n to continue.");
            userInput = Console.ReadLine();
            userInput = userInput.ToLower();
        }
        while (userInput != "y" && userInput != "n")
        {
            Console.WriteLine("Please enter y/n to continue.");
            userInput = Console.ReadLine();
            userInput = userInput.ToLower();

        }
        if (userInput == "y")
        {
            anotherItem = true;
        }
        else if (userInput == "n")
        {
            anotherItem = false;
            Console.Clear();
        }
    }
    return total;

}//end function

//function to get users bills to pay
static decimal GetUserMoney(decimal total, decimal changeNeeded, decimal[,] registerArray)
{
    decimal bill = 0.0M;
    decimal userMoney = 0.0M;
    string userInput;
    decimal remainingBal = total;
    Console.WriteLine("Thank you! Your total is: " + total);


    while (userMoney < total)//keep going until theis bills are great then or equal to their total
    {
        Console.WriteLine("Please enter your bill or coin:");
        userInput = Console.ReadLine();
        if (userInput == "banana" || userInput == "Banana" || userInput == "BANANA")
        {
            SoundPlayer player = new SoundPlayer("you-didn't-say-the-magic-word.wav");
            player.Play();
        }

        while (!decimal.TryParse(userInput, out _) || decimal.Parse(userInput) < 0)
        {
            Console.WriteLine("Invalid Input. Please enter a singular bill or coin:");
            userInput = Console.ReadLine();

        }
        bill = decimal.Parse(userInput);
        while (bill != registerArray[0, 0] && bill != registerArray[0, 1] && bill != registerArray[0, 2] && bill != registerArray[0, 3] && bill != registerArray[0, 4] && bill != registerArray[0, 5] && bill != registerArray[0, 6] && bill != registerArray[0, 7] && bill != registerArray[0, 8] && bill != registerArray[0, 9])
        {
            Console.WriteLine("Counterfeit detect988ed, please use a different bill or coin:");
            userInput = Console.ReadLine();
            while (!decimal.TryParse(userInput, out _) || decimal.Parse(userInput) < 0)
            {
                Console.WriteLine("Invalid Input. Please enter a singular bill or coin:");
                userInput = Console.ReadLine();
            }
            bill = decimal.Parse(userInput);
        }


        userMoney += bill;


        for (int i = 0; i < 9; i++)
        {
            if (bill == registerArray[0, i])
            {
                registerArray[1, i] = registerArray[1, i] + 1;
            }
        }
        remainingBal -= bill;
        Console.Clear();
        Console.WriteLine("Your remaining balance is $" + remainingBal);


    }
    changeNeeded = (userMoney - total);
    return userMoney;

}

//end function


//function that checks register and dispenses change
static void greedyAlgo(decimal changeNeeded, decimal registerTotal, decimal[,] registerArray)
{

    GetRegister(registerArray);

    //for loop to calculate total of the register
    for (int i = 0; i < 10; i++)
    {
        registerTotal += registerArray[0, i] * registerArray[1, i];


    }//end for loop


    //if stements to check if their is enough money in register to make change
    if (changeNeeded <= registerTotal)//if there is enough money, do this
    {
        for (int j = 9; j >= 0; j--)//traverse array to access every dollar values ammount
        {
            while (changeNeeded > 0)//while change is still needed
            {
                if (changeNeeded >= registerArray[0, j] && registerArray[1, j] > 0)// checking for the greatest dollar value then checking that we have enough bills of that dollar value
                {
                    changeNeeded = changeNeeded - registerArray[0, j];//subtract that value from change needed
                    registerArray[1, j] = registerArray[1, j] - 1;//re initialize the value of the ammount of that bill left in the register
                    Console.WriteLine("Money Dispensed: $" + registerArray[0, j]); //display that you dispensed that bill or coin

                }
                else
                {
                    break;
                }


            }
        }

    }
    if (changeNeeded > 0)
    {
        Console.WriteLine("Error. Please see an associate for the rest for your cash.");
    }
    else if (changeNeeded > registerTotal)//else if not enough money, do this
    {
        Console.WriteLine("Im sorry, this register does not have enough cash.");
    }


}//end function

//function to get cc number and assing it to an array




static string GetCreditCardNumber(string creditCardNumber)
{
    int cclength;
    Console.WriteLine("Please enter your Credit Card Number, using number keys only, no spaces.");
    creditCardNumber = Console.ReadLine();
    while (string.IsNullOrEmpty(creditCardNumber))
    {
        Console.WriteLine("Please actually enter your card number. We're not giving anything out for free.");
        creditCardNumber = Console.ReadLine();
    }
    while (creditCardNumber.Contains('a') || creditCardNumber.Contains('b') || creditCardNumber.Contains('c') || creditCardNumber.Contains('d') ||
            creditCardNumber.Contains('e') || creditCardNumber.Contains('f') || creditCardNumber.Contains('g') || creditCardNumber.Contains('h') ||
            creditCardNumber.Contains('i') || creditCardNumber.Contains('j') || creditCardNumber.Contains('k') || creditCardNumber.Contains('m') ||
            creditCardNumber.Contains('o') || creditCardNumber.Contains('p') || creditCardNumber.Contains('q') || creditCardNumber.Contains('n') ||
            creditCardNumber.Contains('r') || creditCardNumber.Contains('s') || creditCardNumber.Contains('t') || creditCardNumber.Contains('u') ||
            creditCardNumber.Contains('v') || creditCardNumber.Contains('w') || creditCardNumber.Contains('x') || creditCardNumber.Contains('z') || creditCardNumber.Contains('y') || creditCardNumber.Contains(' ') || creditCardNumber.Contains('.'))
    {
        Console.WriteLine("There are no letters in a credit card. Please try again.");
        creditCardNumber = Console.ReadLine();
        while (string.IsNullOrEmpty(creditCardNumber))
        {
            Console.WriteLine("Please actually enter your card number. We're not giving anything out for free.");
            creditCardNumber = Console.ReadLine();
        }
    }
    Console.Clear();
    //create string array sized by length 


    cclength = creditCardNumber.Length;//assign cc length to an int


    char[] ccArray = new char[cclength]; //declare array to hold cc numbers 
    creditCardNumber.CopyTo(0, ccArray, 0, creditCardNumber.Length);

    while (cclength != 15 && cclength != 16)
    {
        Console.WriteLine("Please enter a credit card number that is either 15 or 16 digits.");
        creditCardNumber = (Console.ReadLine());

        cclength = creditCardNumber.Length;

        creditCardNumber.CopyTo(0, ccArray, 0, creditCardNumber.Length);
    }
    while (ccArray[0] != '4' && ccArray[0] != '5' && ccArray[0] != '6' && ccArray[0] != '3')
    {
        Console.WriteLine("Sorry, we dont not accpet this card type. Please enter another.");
        creditCardNumber = (Console.ReadLine());
        ccArray = new char[cclength];//declare array to hold cc numbers
        while (cclength != 15 && cclength != 16)
        {
            Console.WriteLine("Please enter a credit card number that is either 15 or 16 digits.");
            creditCardNumber = (Console.ReadLine());

            ccArray = new char[cclength];//declare array to hold cc numbers 

        }
        creditCardNumber.CopyTo(0, ccArray, 0, creditCardNumber.Length);
    }
    Console.Clear();

    return creditCardNumber;
}
//end function\\


//function to validate card
static bool ValidCardNumber(string CreditCardNumber, int ccLength)
{

    int sum = 0;
    ccLength = CreditCardNumber.Length;


    string[] ccArraystring = new string[ccLength];
    char[] ccArraychar = new char[ccLength];
    CreditCardNumber.CopyTo(0, ccArraychar, 0, ccLength);
    int[] intccArray = new int[ccLength];
    for (int k = 0; k < ccLength; k++)
    {
        ccArraystring[k] = ccArraychar[k].ToString();
        intccArray[k] = int.Parse(ccArraystring[k]);
    }

    for (int i = 0; i < ccLength; i += 2)
    {
        intccArray[i] = intccArray[i] * 2;
        if (intccArray[i] > 9)
        {
            string temp = intccArray[i].ToString();
            int something = int.Parse(temp[0].ToString()) + int.Parse(temp[1].ToString());
            intccArray[i] = something;
        }

    }

    for (int j = 0; j < ccArraystring.Length; j++)
    {
        sum += intccArray[j];

    }

    if (sum % 10 == 0)
    {
        return true;
    }
    else
    {
        return false;
    }

}

//end function 


//function that figures out what type of card
static string whatTypeOfCard(string creditCardNumber, string cardType)
{
    char[] ccArray = new char[creditCardNumber.Length];
    creditCardNumber.CopyTo(0, ccArray, 0, creditCardNumber.Length);

    if (ccArray[0] == '4' && ccArray.Length == 16)
    {
        cardType = "Visa";
    }
    if (ccArray[0] == '5' && ccArray.Length == 16)
    {
        cardType = "Mastercard";
    }
    if (ccArray[0] == '6' && ccArray.Length == 16)
    {
        cardType = "Discover Card";
    }
    if (ccArray[0] == '3' && ccArray.Length == 15)
    {
        cardType = "American Express";
    }
    else
    {

    }

    return cardType;

}





static string[] MoneyRequest(string account_number, decimal amount)
{
    Random rnd = new Random();
    //50 percent chance pass or fail
    bool pass = rnd.Next(100) < 50;
    //50 percent chance that a fail transaction is declined
    bool declined = rnd.Next(100) < 50;

    if (pass)
    {
        return new string[] { account_number, amount.ToString() };

    }
    else
    {
        if (!declined)
        {
            return new string[] { account_number, (amount / rnd.Next(2, 6)).ToString() };
        }
        else
        {
            return new string[] { account_number, "declined" };
        }
    }
}

static decimal[,] GetRegister(decimal[,] registerArray)
{
    register Ones;
    register Fives;
    register Tens;
    register Twenties;
    register Fifties;
    register Hundreds;
    register Pennies;
    register Nickles;
    register Dimes;
    register Quarters;

    Ones.ones = 1;
    Ones.ones_amount = 50;
    Fives.fives = 5;
    Fives.fives_amount = 20;
    Tens.tens = 10;
    Tens.tens_amount = 10;
    Twenties.twenties = 20;
    Twenties.twenties_amount = 5;
    Fifties.fifties = 50;
    Fifties.fifties_amount = 2;
    Hundreds.hundreds = 100;
    Hundreds.hundreds_amount = 1;
    Pennies.pennies = 0.01M;
    Pennies.pennies_amount = 100;
    Nickles.nickles = 0.05M;
    Nickles.nickles_amount = 30;
    Dimes.dimes = 0.10M;
    Dimes.dimes_amount = 20;
    Quarters.quarters = 0.25M;
    Quarters.quarters_amount = 15;

    registerArray = new decimal[2, 10]  { {Pennies.pennies, Nickles.nickles, Dimes.dimes, Quarters.quarters, Ones.ones, Fives.fives, Tens.tens, Twenties.twenties, Fifties.fifties, Hundreds.hundreds } ,
    { Pennies.pennies_amount, Nickles.nickles_amount, Dimes.dimes_amount, Quarters.quarters_amount, Ones.ones_amount, Fives.fives_amount, Tens.tens_amount, Twenties.twenties_amount, Fifties.fifties_amount, Hundreds.hundreds_amount} };

    return registerArray;


}
struct register
{
    public decimal ones;
    public decimal ones_amount;
    public decimal fives;
    public decimal fives_amount;
    public decimal tens;
    public decimal tens_amount;
    public decimal twenties;
    public decimal twenties_amount;
    public decimal fifties;
    public decimal fifties_amount;
    public decimal hundreds;
    public decimal hundreds_amount;
    public decimal pennies;
    public decimal pennies_amount;
    public decimal nickles;
    public decimal nickles_amount;
    public decimal dimes;
    public decimal dimes_amount;
    public decimal quarters;
    public decimal quarters_amount;
}