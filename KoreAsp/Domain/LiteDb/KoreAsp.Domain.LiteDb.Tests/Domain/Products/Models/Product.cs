﻿// ***********************************************************************
// <copyright file="Product.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.ComponentModel.DataAnnotations;
using System.Linq;
using KoreAsp.Domain.Context;

namespace KoreAsp.Domain.LiteDb.Tests.Domain.Products.Models
{
    /// <summary>
    /// A test Product model for the Core.EF Testing Suite
    /// </summary>
    /// <seealso cref="KoreAsp.Domain.Context.BaseEntity" />
    /// <seealso cref="KoreAsp.Domain.Context.IEntity" />
    public class Product : BaseEntity, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public Product(IRepository repository, string name, string description)
        {
            this.Name = name;
            this.Description = description;
            this.EntityState = DomainState.Added;
            this.DisplayOrder = repository.Get<Product>().Select(x => x.DisplayOrder).DefaultIfEmpty(0).Max() + 1;
            repository.Add(this);
        }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        public string ProductId
        {
            get
            {
                return this.UniqueId;
            }

            set
            {
                this.UniqueId = value;
            }
        }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>The category identifier.</value>
        /// <remarks>Normally this property would be immutable and set only within this class, but for testing purposes, the setter is left public.</remarks>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        /// <remarks>Normally this property would be immutable and set only within this class, but for testing purposes, the setter is left public.</remarks>
        [Required]
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        /// <value>The display order.</value>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public void Update(IRepository repository)
        {
            this.EntityState = DomainState.Modified;
            repository.Update(this);
        }

        /// <summary>
        /// Updates the description.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="description">The description.</param>
        public void UpdateDescription(IRepository repository, string description)
        {
            this.Description = description;
            this.Update(repository);
        }

        /// <summary>
        /// Moves up.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public void MoveUp(IRepository repository)
        {
            Product prev = repository.Get<Product>().Where(x => x.DisplayOrder < this.DisplayOrder).OrderByDescending(x => x.DisplayOrder).FirstOrDefault();
            if (prev != null)
            {
                int prevOrder = prev.DisplayOrder;
                prev.DisplayOrder = this.DisplayOrder;
                this.DisplayOrder = prevOrder;
                this.Update(repository);
                repository.Update(prev);
            }
        }

        /// <summary>
        /// Moves down.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public void MoveDown(IRepository repository)
        {
            Product next = repository.Get<Product>().Where(x => x.DisplayOrder > this.DisplayOrder).OrderBy(x => x.DisplayOrder).FirstOrDefault();
            if (next != null)
            {
                int nextOrder = next.DisplayOrder;
                next.DisplayOrder = this.DisplayOrder;
                this.DisplayOrder = nextOrder;
                this.Update(repository);
                repository.Update(next);
            }
        }
    }
}
