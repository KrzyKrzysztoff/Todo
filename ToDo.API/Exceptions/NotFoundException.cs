﻿namespace ToDo.API.Exceptions
{
    public class NotFoundException(string message = "Requested resource was not found.") : Exception(message)
    {
    }
}
