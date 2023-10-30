﻿using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public void Complete(IDateProvider dateProvider)
        {
            if(dateProvider.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                IsCompleted = true;
            }
            else
            {
                throw new ValidationException("Task can only be complete on Thursday.");
            } 
        }
    }
}
