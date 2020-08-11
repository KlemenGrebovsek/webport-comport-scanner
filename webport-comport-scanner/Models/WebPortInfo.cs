﻿namespace webport_comport_scanner.Models
{
    public class WebPortInfo : IPrintableScanResult
    {
        private int port;
        private PortStatus status;

        public WebPortInfo(int port, PortStatus status)
        {
            this.port = port;
            this.status = status;
        }

        public PortStatus GetPortStatus()
        {
            return status;
        }

        public string GetName()
        {
            return port.ToString();
        }

        public string GetStatus()
        {
            return status.ToString();
        }

        public PortStatus GetStatusRaw()
        {
            return status;
        }

        public int GetMaxPrintLenght()
        {
            int nameLen = port.ToString().Length;
            int statusLen = status.ToString().Length;

            return (nameLen > statusLen) ? nameLen : statusLen;
        }

    }  
}
