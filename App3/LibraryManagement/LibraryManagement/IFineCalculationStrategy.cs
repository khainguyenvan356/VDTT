using System;

namespace LibraryManagement
{
    public interface IFineCalculationStrategy
    {
        decimal CalculateFine(DateTime dueDate, DateTime returnDate);
    }


    public class EarlyFineStrategy : IFineCalculationStrategy
    {
        public decimal CalculateFine(DateTime dueDate, DateTime returnDate)
        {
            if (returnDate <= dueDate) return 0;

            int daysLate = (returnDate - dueDate).Days;
            if (daysLate > 14) return 0; 

            return daysLate * 1000;
        }
    }

    public class MidFineStrategy : IFineCalculationStrategy
    {
        public decimal CalculateFine(DateTime dueDate, DateTime returnDate)
        {
            if (returnDate <= dueDate) return 0;

            int daysLate = (returnDate - dueDate).Days;
            if (daysLate <= 14 || daysLate > 28) return 0;

            int earlyDays = 14;
            int midDays = daysLate - earlyDays;
            return (earlyDays * 1000) + (midDays * 1500);
        }
    }

    public class LateFineStrategy : IFineCalculationStrategy
    {
        public decimal CalculateFine(DateTime dueDate, DateTime returnDate)
        {
            if (returnDate <= dueDate) return 0;

            int daysLate = (returnDate - dueDate).Days;
            if (daysLate <= 28) return 0; 
            int earlyDays = 14;
            int midDays = 14;
            int lateDays = daysLate - (earlyDays + midDays);
            return (earlyDays * 1000) + (midDays * 1500) + (lateDays * 2000);
        }
    }

    public class FineStrategyManager
    {
        private readonly IFineCalculationStrategy[] _strategies;

        public FineStrategyManager()
        {
            _strategies = new IFineCalculationStrategy[]
            {
                new EarlyFineStrategy(),
                new MidFineStrategy(),
                new LateFineStrategy()
            };
        }

        public decimal CalculateTotalFine(DateTime dueDate, DateTime returnDate)
        {
            decimal totalFine = 0;
            int daysLate = (returnDate - dueDate).Days;

            if (daysLate > 0)
            {
                foreach (var strategy in _strategies)
                {
                    decimal fine = strategy.CalculateFine(dueDate, returnDate);
                    totalFine += fine;
                }
            }

            return totalFine;
        }
    }
}