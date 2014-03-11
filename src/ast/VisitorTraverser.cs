﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using crosspascal.ast.nodes;

namespace crosspascal.ast
{
	// Delivers a visitor pattern with which to traverse the AST

	class VisitorTraverser : Traverser
	{
		public VisitorTraverser() : base() { }

		public VisitorTraverser(Processor processor) : base(processor) { }

		public override void traverse(Node n)
		{
			if (n != null)
				n.Accept(Processor);
		}
	}
}