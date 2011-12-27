namespace IntergalacticControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using IntergalacticCore.Data;

    /// <summary>
    /// Has info regarding the type and limits of an operation input.
    /// </summary>
    public class OperationInputInfo
    {
        /// <summary>
        /// The input data type.
        /// </summary>
        private InputType type;

        /// <summary>
        /// Input title.
        /// </summary>
        private string title;

        /// <summary>
        /// Input limits (for numeric input only).
        /// </summary>
        private object from = null, to = null;

        /// <summary>
        /// Initializes a new instance of the OperationInputInfo class.
        /// </summary>
        /// <param name="title">Input title</param>
        /// <param name="type">Input data type</param>
        /// <param name="from">Input 'from' value (for numeric input only.  Otherwise null).</param>
        /// <param name="to">Input 'to' value (for numeric input only.  Otherwise null).</param>
        public OperationInputInfo(string title, InputType type, object from, object to)
        {
            if (type == InputType.Bool || type == InputType.Color || type == InputType.Image || type == InputType.DoubleMask || type == InputType.BinaryMask)
            {
                if (from != null || to != null)
                {
                    throw new InvalidOperationException("Selected input type only accepts null values.");
                }
            }
            else if (from == null || to == null)
            {
                throw new InvalidOperationException("Selected input type does not accept null values.");
            }
            else
            {
                if (from.GetType() != to.GetType())
                {
                    throw new InvalidOperationException("The entered \"from\" and \"to\" types are different from one another.");
                }
                else
                {
                    if (type != this.GetInputType(from))
                    {
                        if (type != InputType.DoubleArray || this.GetInputType(from) != InputType.Double)
                        {
                            throw new InvalidOperationException("The entered \"from\" and \"to\" types are different from the given type.");
                        }
                    }
                }

                this.from = from;
                this.to = to;
            }

            this.title = title;
            this.type = type;
        }

        /// <summary>
        /// Gets the input data type
        /// </summary>
        public InputType Type
        {
            get { return this.type; }
        }

        /// <summary>
        /// Gets the input title
        /// </summary>
        public string Title
        {
            get { return this.title; }
        }

        /// <summary>
        /// Gets or sets the 'from' value
        /// </summary>
        public object From
        {
            get { return this.from; }
            set { this.from = value; }
        }

        /// <summary>
        /// Gets or sets the 'to' value
        /// </summary>
        public object To
        {
            get { return this.to; }
            set { this.to = value; }
        }

        /// <summary>
        /// Gets the input type for an object
        /// </summary>
        /// <param name="obj">Object to be tested</param>
        /// <returns>Input type</returns>
        private InputType GetInputType(object obj)
        {
            bool found = false;
            InputType result = InputType.Bool;

            bool boolean = false;
            byte b = 0;
            int i = 0;
            float f = 0;
            double d = 0;
            Pixel color;
            ConvolutionMask mask;
            ImageBase img;

            try
            {
                boolean = (bool)obj;
                found = true;
                result = InputType.Bool;
            }
            catch (Exception)
            {
            }

            try
            {
                b = (byte)obj;
                found = true;
                result = InputType.Byte;
            }
            catch (Exception)
            {
            }

            try
            {
                i = (int)obj;
                found = true;
                result = InputType.Int;
            }
            catch (Exception)
            {
            }

            try
            {
                f = (float)obj;
                found = true;
                result = InputType.Float;
            }
            catch (Exception)
            {
            }

            try
            {
                d = (double)obj;
                found = true;
                result = InputType.Double;
            }
            catch (Exception)
            {
            }

            try
            {
                color = (Pixel)obj;
                found = true;
                result = InputType.Color;
            }
            catch (Exception)
            {
            }

            try
            {
                img = (ImageBase)obj;
                found = true;
                result = InputType.Image;
            }
            catch (Exception)
            {
            }

            try
            {
                mask = (ConvolutionMask)obj;
                found = true;
                result = InputType.DoubleMask;
            }
            catch (Exception)
            {
            }

            if (!found)
            {
                throw new Exception("Unsupported data type.");
            }

            return result;
        }
    }
}
