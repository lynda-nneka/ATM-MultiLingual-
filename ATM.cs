using System;
namespace MultiATM
{
	public class ATM
	{
		public int userCash = 10000;
		public string userPin = "1234";
		public OperationType operationType;
		public ATMLanguage ATMLanguage;

		public ATM(ATMLanguage atmLanguage)
		{
			this.ATMLanguage = atmLanguage;
		}

        public virtual void Start()
		{
			Console.WriteLine(ATMLanguage.welcomeMessage);

			var isPinCorrect = PinCheck();

			if (isPinCorrect)
			{
				Console.WriteLine(ATMLanguage.selectOperationMessage);

                var operationInput = Console.ReadLine();
                var numberSelected = Int32.Parse(operationInput);

                operationType = (OperationType)numberSelected;

                Console.WriteLine(operationType);


                switch (operationType)
                {
                    case OperationType.Withdraw:
                        Withdraw();
                        break;
                    case OperationType.CheckBalance:
                        CheckBalance();
                        break;
                    case OperationType.Transfer:
                        Transfer();
                        break;
                }
            } else
			{
				Console.WriteLine(ATMLanguage.pinIncorrectMessage);
				Restart();
			}
			
		}

		// Main Logic

		private bool PinCheck()
		{
			Console.WriteLine(ATMLanguage.enterATMPinMessage);
			var atmInput = Console.ReadLine();

			if (atmInput == userPin)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void CheckBalance()
		{
			CheckBalanceOperation();
		}

		private void Transfer()
		{
			Console.WriteLine(ATMLanguage.enterBankNameMessage);
			var bankName = Console.ReadLine();

			Console.WriteLine(ATMLanguage.enterAccountNumberMessage);
			var accountNumber = Console.ReadLine();

            Console.WriteLine(ATMLanguage.enterAmountMessage);
            var amountInput = Console.ReadLine();
			var amount = Int32.Parse(amountInput);

			TransferOperation(amount, bankName, accountNumber);
        }

        private void Withdraw()
		{
			Console.WriteLine(ATMLanguage.withdrawAmountMessage);
			var withdrawInput = Console.ReadLine();
			var withdrawAmount = Int32.Parse(withdrawInput);

			WithdrawOperation(withdrawAmount);
		}

		// Operation Logic

		private void CheckBalanceOperation()
		{
            Console.WriteLine(ATMLanguage.accountBalanceMessage + userCash);

            Restart();
        }

		private void TransferOperation(int transferAmount, string bankName, string accountNumber)
		{
			if (transferAmount > userCash)
			{
				Console.WriteLine(ATMLanguage.insufficientFundsMessage);
			}
			else
			{
                userCash -= transferAmount;
                Console.WriteLine(ATMLanguage.successfulTransferMessage + accountNumber);
                Console.WriteLine(ATMLanguage.accountBalanceMessage + userCash);
            }

			Restart();
		}

		private void WithdrawOperation(int amount)
		{
			if (amount > userCash)
			{
				Console.WriteLine(ATMLanguage.insufficientFundsMessage);
			}
			else
			{
				userCash -= amount;
				Console.WriteLine(ATMLanguage.withdrawSuccessfulMessage);
				Console.WriteLine(ATMLanguage.amountLeftMessage + userCash);
			}

			Restart();
		}

		private void Restart()
		{
			Console.WriteLine(ATMLanguage.anotherOperationMessage);
			var input = Console.ReadLine();

			if (input.ToUpper() == "Y")
			{
				Start();
			}
			else
			{
				Console.WriteLine(ATMLanguage.takeYourCardMessage);
			}
		}
	}

	public enum OperationType
	{
		Withdraw = 1, CheckBalance = 2, Transfer = 3 
	}
}

