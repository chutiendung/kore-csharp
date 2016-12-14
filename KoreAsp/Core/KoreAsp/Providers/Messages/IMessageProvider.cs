﻿// ***********************************************************************
// <copyright file="IMessageProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using KoreAsp.Providers.Logging;

namespace KoreAsp.Providers.Messages
{
    /// <summary>
    /// A contract for a provider of messages generated by the domain or service to pass down to the
    /// application that is a consumer of the service layer.
    /// </summary>
    public interface IMessageProvider
    {
        #region Properties

        /// <summary>
        /// Gets the logger.
        /// </summary>
        ILoggingProvider Logger { get; }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        IEnumerable<Message> Messages { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>s
        bool HasErrors { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a collection of just the message texts of a specific type.
        /// </summary>
        /// <param name="type">The type of the message.</param>
        /// <returns>The collection of message texts.</returns>
        IEnumerable<string> GetMessages(MessageType type);

        /// <summary>
        /// Adds the exception message if debugging. If not in debug mode, it simply indicates that there
        /// was an application error and provides the user with a reference ID to use when contacting support
        /// (within Message) that will hopefully be logged along with the real exception.
        /// </summary>
        /// <param name="exception">The exception message.</param>
        void AddException(Exception exception);

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        Message AddMessage(MessageType type, string message, params object[] param);

        /// <summary>
        /// Adds the message and identifies the property it relates to in order to assist in marking the property in the UI.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="property">The property.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        Message AddMessageByProperty(MessageType type, string message, string property, params object[] param);

        /// <summary>
        /// Adds the message and identifies the property it relates to in order to assist in marking the property in the UI.
        /// </summary>
        /// <typeparam name="T">The type of the model that contains the property to select.</typeparam>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        Message AddMessageByProperty<T>(MessageType type, string message, Expression<Func<T, object>> propertySelector, params object[] param);

        /// <summary>
        /// Adds the message and identifies the property and record unique ID it relates to in order to assist in marking the property in the UI.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="property">The property.</param>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        Message AddMessageByPropertyAndUniqueId(MessageType type, string message, string property, string uniqueId, params object[] param);

        /// <summary>
        /// Adds the message and identifies the property and record unique ID it relates to in order to assist in marking the property in the UI.
        /// </summary>
        /// <typeparam name="T">The type of the model that contains the property to select.</typeparam>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        Message AddMessageByPropertyAndUniqueId<T>(MessageType type, string message, Expression<Func<T, object>> propertySelector, string uniqueId, params object[] param);

        /// <summary>
        /// Removes the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Whether the message was removed.</returns>
        bool RemoveMessage(Message message);

        /// <summary>
        /// Clears all messages.
        /// </summary>
        void ClearAllMessages();

        #endregion
    }
}
