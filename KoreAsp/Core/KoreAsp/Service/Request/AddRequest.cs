﻿// ***********************************************************************
// <copyright file="AddRequest.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

namespace KoreAsp.Service.Request
{
    /// <summary>
    /// Encapsulates the contents of a service add request which is intended to create or update something
    /// </summary>
    /// <seealso cref="KoreAsp.Service.Request.Request" />
    /// <seealso cref="KoreAsp.Service.Request.IRequest" />
    public class AddRequest : Request, IRequest
    {
    }

    /// <summary>
    /// Encapsulates the contents of a service add request which is intended to create or update something
    /// </summary>
    /// <typeparam name="T">The model type</typeparam>
    /// <seealso cref="KoreAsp.Service.Request.Request" />
    /// <seealso cref="KoreAsp.Service.Request.IRequest" />
    public class AddRequest<T> : Request, IRequest
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public T Model { get; set; }
    }
}
