// Copyright (c) 2012-2023 Wojciech Figat. All rights reserved.

using System;

namespace FlaxEngine
{
    /// <summary>
    /// Virtual input axis binding. Helps with listening for a selected axis input.
    /// </summary>
    public class InputAxis
    {
        /// <summary>
        /// The name of the axis to use. See <see cref="Input.AxisMappings"/>.
        /// </summary>
        [Tooltip("The name of the axis to use.")]
        public string Name;

        /// <summary>
        /// Gets the current axis value.
        /// </summary>
        public float Value => Input.GetAxis(Name);

        /// <summary>
        /// Gets the current axis raw value.
        /// </summary>
        public float ValueRaw => Input.GetAxisRaw(Name);

        /// <summary>
        /// Occurs when axis is changed. Called before scripts update.
        /// </summary>
        public event Action ValueChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputAxis"/> class.
        /// </summary>
        public InputAxis()
        {
            Input.AxisChanged += Handler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputAxis"/> class.
        /// </summary>
        /// <param name="name">The axis name.</param>
        public InputAxis(string name)
        {
            Input.AxisChanged += Handler;
            Name = name;
        }
        
        private void Handler(string name)
        {
            if (string.Equals(Name, name, StringComparison.OrdinalIgnoreCase))
                ValueChanged?.Invoke();
        }
        
        /// <summary>
        /// Finalizes an instance of the <see cref="InputAxis"/> class.
        /// </summary>
        ~InputAxis()
        {
            Input.AxisChanged -= Handler;
        }
        
        /// <summary>
        /// Releases this object.
        /// </summary>
        public void Dispose()
        {
            Input.AxisChanged -= Handler;
            GC.SuppressFinalize(this);
        }
    }
}
