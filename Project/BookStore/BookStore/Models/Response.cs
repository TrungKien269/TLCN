﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Response
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public int? Line { get; set; }
        public object Obj { get; set; }
        public string previousState { get; set; }

        public Response() { }

        public Response(string message, bool status, int line, object obj, string state)
        {
            this.Line = line;
            this.Message = message;
            this.Status = status;
            this.Obj = obj;
            this.previousState = state;
        }

        public Response(string message, bool status, int line, object obj)
        {
            this.Line = line;
            this.Message = message;
            this.Status = status;
            this.Obj = obj;
            this.previousState = null;
        }

        public static Response CatchError(string message)
        {
            return new Response(message, false, 0, null);
        }
    }
}
