﻿using System;

public class Message
{
	public string Body { get; set; }
	public string Signature { get; set; }
	public string SessionKey { get; set; }
	public string InitVector { get; set; }
	public DateTime DateSent { get; set; }
	public int SenderId { get; set; }
	public int ReceiverId { get; set; }
}