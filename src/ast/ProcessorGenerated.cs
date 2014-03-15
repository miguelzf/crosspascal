// ======================================================================
// Base Processor/Visitor class, auto-generated							
// 	Do NOT edit this file												
// 	Aditional methods should be defined in another file					
// ======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace crosspascal.ast
{
	public abstract partial class Processor
	{
		//	Complete interface to be implemented by any specific AST processor	
		
		public virtual void Visit(CompositeDeclaration node)
		{
		}
				
		public virtual void Visit(ClassBody node)
		{
			traverse(node.fields);
			traverse(node.content);
		}
				
		public virtual void Visit(ClassDefinition node)
		{
			traverse(node.ClassBody);
		}
				
		public virtual void Visit(InterfaceDefinition node)
		{
			traverse(node.methods);
			traverse(node.properties);
		}
				
		public virtual void Visit(ClassContent node)
		{
		}
				
		public virtual void Visit(ClassMethod node)
		{
			traverse(node.decl);
		}
				
		public virtual void Visit(ClassProperty node)
		{
			traverse(node.type);
			traverse(node.index);
			traverse(node.specs);
			traverse(node.def);
		}
				
		public virtual void Visit(PropertyReadNode node)
		{
		}
				
		public virtual void Visit(PropertyWriteNode node)
		{
		}
				
		public virtual void Visit(PropertySpecifiers node)
		{
			traverse(node.index);
			traverse(node.read);
			traverse(node.write);
			traverse(node.stored);
			traverse(node.def);
			traverse(node.impl);
		}
				
		public virtual void Visit(PropertySpecifier node)
		{
		}
				
		public virtual void Visit(PropertyDefault node)
		{
			traverse(node.lit);
		}
				
		public virtual void Visit(PropertyImplements node)
		{
		}
				
		public virtual void Visit(PropertyStored node)
		{
		}
				
		public virtual void Visit(PropertyIndex node)
		{
		}
				
		public virtual void Visit(Declaration node)
		{
			traverse(node.type);
		}
				
		public virtual void Visit(LabelDeclaration node)
		{
		}
				
		public virtual void Visit(VarDeclaration node)
		{
			traverse(node.init);
		}
				
		public virtual void Visit(ParameterDeclaration node)
		{
		}
				
		public virtual void Visit(VarParameterDeclaration node)
		{
		}
				
		public virtual void Visit(ConstParameterDeclaration node)
		{
		}
				
		public virtual void Visit(OutParameterDeclaration node)
		{
		}
				
		public virtual void Visit(FieldDeclaration node)
		{
		}
				
		public virtual void Visit(ConstDeclaration node)
		{
			traverse(node.init);
		}
				
		public virtual void Visit(EnumValue node)
		{
		}
				
		public virtual void Visit(TypeDeclaration node)
		{
		}
				
		public virtual void Visit(CompositeDeclaration node)
		{
		}
				
		public virtual void Visit(CallableDeclaration node)
		{
		}
				
		public virtual void Visit(Expression node)
		{
			traverse(node.Type);
		}
				
		public virtual void Visit(EmptyExpression node)
		{
		}
				
		public virtual void Visit(ExpressionList node)
		{
		}
				
		public virtual void Visit(ConstExpression node)
		{
			traverse(node.expr);
		}
				
		public virtual void Visit(StructuredConstant node)
		{
		}
				
		public virtual void Visit(ArrayConst node)
		{
		}
				
		public virtual void Visit(RecordConst node)
		{
		}
				
		public virtual void Visit(FieldInitList node)
		{
		}
				
		public virtual void Visit(FieldInit node)
		{
		}
				
		public virtual void Visit(BinaryExpression node)
		{
		}
				
		public virtual void Visit(SetIn node)
		{
			traverse(node.expr);
			traverse(node.set);
		}
				
		public virtual void Visit(SetRange node)
		{
		}
				
		public virtual void Visit(ArithmethicBinaryExpression node)
		{
			traverse(node.left);
			traverse(node.right);
		}
				
		public virtual void Visit(Subtraction node)
		{
		}
				
		public virtual void Visit(Addition node)
		{
		}
				
		public virtual void Visit(Product node)
		{
		}
				
		public virtual void Visit(Division node)
		{
		}
				
		public virtual void Visit(Quotient node)
		{
		}
				
		public virtual void Visit(Modulus node)
		{
		}
				
		public virtual void Visit(ShiftRight node)
		{
		}
				
		public virtual void Visit(ShiftLeft node)
		{
		}
				
		public virtual void Visit(LogicalBinaryExpression node)
		{
			traverse(node.left);
			traverse(node.right);
		}
				
		public virtual void Visit(LogicalAnd node)
		{
		}
				
		public virtual void Visit(LogicalOr node)
		{
		}
				
		public virtual void Visit(LogicalXor node)
		{
		}
				
		public virtual void Visit(Equal node)
		{
		}
				
		public virtual void Visit(NotEqual node)
		{
		}
				
		public virtual void Visit(LessThan node)
		{
		}
				
		public virtual void Visit(LessOrEqual node)
		{
		}
				
		public virtual void Visit(GreaterThan node)
		{
		}
				
		public virtual void Visit(GreaterOrEqual node)
		{
		}
				
		public virtual void Visit(TypeBinaryExpression node)
		{
			traverse(node.expr);
			traverse(node.types);
		}
				
		public virtual void Visit(TypeIs node)
		{
		}
				
		public virtual void Visit(TypeCast node)
		{
		}
				
		public virtual void Visit(UnaryExpression node)
		{
		}
				
		public virtual void Visit(SimpleUnaryExpression node)
		{
			traverse(node.expr);
		}
				
		public virtual void Visit(UnaryPlus node)
		{
		}
				
		public virtual void Visit(UnaryMinus node)
		{
		}
				
		public virtual void Visit(LogicalNot node)
		{
		}
				
		public virtual void Visit(AddressLvalue node)
		{
		}
				
		public virtual void Visit(Set node)
		{
			traverse(node.setelems);
		}
				
		public virtual void Visit(ConstantValue node)
		{
		}
				
		public virtual void Visit(IntegralValue node)
		{
		}
				
		public virtual void Visit(StringValue node)
		{
		}
				
		public virtual void Visit(RealValue node)
		{
		}
				
		public virtual void Visit(Literal node)
		{
		}
				
		public virtual void Visit(OrdinalLiteral node)
		{
		}
				
		public virtual void Visit(IntLiteral node)
		{
		}
				
		public virtual void Visit(CharLiteral node)
		{
		}
				
		public virtual void Visit(BoolLiteral node)
		{
		}
				
		public virtual void Visit(StringLiteral node)
		{
		}
				
		public virtual void Visit(RealLiteral node)
		{
		}
				
		public virtual void Visit(PointerLiteral node)
		{
		}
				
		public virtual void Visit(LvalueExpression node)
		{
		}
				
		public virtual void Visit(ArrayAccess node)
		{
			traverse(node.lvalue);
			traverse(node.acessors);
			traverse(node.array);
		}
				
		public virtual void Visit(PointerDereference node)
		{
			traverse(node.expr);
		}
				
		public virtual void Visit(InheritedCall node)
		{
			traverse(node.call);
		}
				
		public virtual void Visit(RoutineCall node)
		{
			traverse(node.func);
			traverse(node.args);
			traverse(node.basictype);
		}
				
		public virtual void Visit(FieldAcess node)
		{
			traverse(node.obj);
		}
				
		public virtual void Visit(Identifier node)
		{
		}
				
		public virtual void Visit(Node node)
		{
		}
				
		public virtual void Visit(FixmeNode node)
		{
		}
				
		public virtual void Visit(NotSupportedNode node)
		{
		}
				
		public virtual void Visit(EmptyNode node)
		{
		}
				
		public virtual void Visit(ListNode node)
		{
		}
				
		public virtual void Visit(NodeList node)
		{
		}
				
		public virtual void Visit(StatementList node)
		{
		}
				
		public virtual void Visit(TypeList node)
		{
		}
				
		public virtual void Visit(IntegralTypeList node)
		{
		}
				
		public virtual void Visit(IdentifierList node)
		{
		}
				
		public virtual void Visit(DeclarationList node)
		{
		}
				
		public virtual void Visit(EnumValueList node)
		{
		}
				
		public virtual void Visit(ParameterList node)
		{
		}
				
		public virtual void Visit(FieldList node)
		{
		}
				
		public virtual void Visit(Processor node)
		{
		}
				
		public virtual void Visit(ProceduralType node)
		{
			traverse(node.@params);
			traverse(node.funcret);
			traverse(node.Directives{get);
		}
				
		public virtual void Visit(MethodType node)
		{
		}
				
		public virtual void Visit(CallableDeclaration node)
		{
			traverse(node.Type{get);
		}
				
		public virtual void Visit(RoutineDeclaration node)
		{
		}
				
		public virtual void Visit(MethodDeclaration node)
		{
		}
				
		public virtual void Visit(SpecialMethodDeclaration node)
		{
		}
				
		public virtual void Visit(ConstructorDeclaration node)
		{
		}
				
		public virtual void Visit(DestructorDeclaration node)
		{
		}
				
		public virtual void Visit(RoutineDefinition node)
		{
			traverse(node.header);
			traverse(node.body);
		}
				
		public virtual void Visit(CallableDirectives node)
		{
		}
				
		public virtual void Visit(RoutineDirectives node)
		{
		}
				
		public virtual void Visit(MethodDirectives node)
		{
		}
				
		public virtual void Visit(ExternalDirective node)
		{
			traverse(node.File);
		}
				
		public virtual void Visit(CompilationUnit node)
		{
		}
				
		public virtual void Visit(ProgramNode node)
		{
			traverse(node.body);
			traverse(node.uses);
		}
				
		public virtual void Visit(LibraryNode node)
		{
			traverse(node.body);
			traverse(node.uses);
		}
				
		public virtual void Visit(UnitNode node)
		{
			traverse(node.interfce);
			traverse(node.implementation);
			traverse(node.init);
		}
				
		public virtual void Visit(PackageNode node)
		{
			traverse(node.requires);
			traverse(node.contains);
		}
				
		public virtual void Visit(UnitItem node)
		{
		}
				
		public virtual void Visit(UsesItem node)
		{
		}
				
		public virtual void Visit(RequiresItem node)
		{
		}
				
		public virtual void Visit(ContainsItem node)
		{
		}
				
		public virtual void Visit(ExportItem node)
		{
		}
				
		public virtual void Visit(Section node)
		{
			traverse(node.decls);
		}
				
		public virtual void Visit(CodeSection node)
		{
			traverse(node.block);
		}
				
		public virtual void Visit(ProgramBody node)
		{
		}
				
		public virtual void Visit(RoutineBody node)
		{
		}
				
		public virtual void Visit(InitializationSection node)
		{
		}
				
		public virtual void Visit(FinalizationSection node)
		{
		}
				
		public virtual void Visit(DeclarationSection node)
		{
			traverse(node.uses);
		}
				
		public virtual void Visit(InterfaceSection node)
		{
		}
				
		public virtual void Visit(ImplementationSection node)
		{
		}
				
		public virtual void Visit(AssemblerRoutineBody node)
		{
		}
				
		public virtual void Visit(Statement node)
		{
		}
				
		public virtual void Visit(LabelStatement node)
		{
			traverse(node.stmt);
		}
				
		public virtual void Visit(EmptyStatement node)
		{
		}
				
		public virtual void Visit(BreakStatement node)
		{
		}
				
		public virtual void Visit(ContinueStatement node)
		{
		}
				
		public virtual void Visit(Assignement node)
		{
			traverse(node.lvalue);
			traverse(node.expr);
		}
				
		public virtual void Visit(GotoStatement node)
		{
		}
				
		public virtual void Visit(IfStatement node)
		{
			traverse(node.condition);
			traverse(node.thenblock);
			traverse(node.elseblock);
		}
				
		public virtual void Visit(ExpressionStatement node)
		{
			traverse(node.expr);
		}
				
		public virtual void Visit(CaseSelector node)
		{
			traverse(node.list);
			traverse(node.stmt);
		}
				
		public virtual void Visit(CaseStatement node)
		{
			traverse(node.condition);
			traverse(node.selectors);
			traverse(node.caseelse);
		}
				
		public virtual void Visit(LoopStatement node)
		{
			traverse(node.condition);
			traverse(node.block);
		}
				
		public virtual void Visit(RepeatLoop node)
		{
		}
				
		public virtual void Visit(WhileLoop node)
		{
		}
				
		public virtual void Visit(ForLoop node)
		{
			traverse(node.var);
			traverse(node.start);
			traverse(node.end);
		}
				
		public virtual void Visit(BlockStatement node)
		{
			traverse(node.stmts);
		}
				
		public virtual void Visit(WithStatement node)
		{
			traverse(node.with);
			traverse(node.body);
		}
				
		public virtual void Visit(TryFinallyStatement node)
		{
			traverse(node.body);
			traverse(node.final);
		}
				
		public virtual void Visit(TryExceptStatement node)
		{
			traverse(node.body);
			traverse(node.final);
		}
				
		public virtual void Visit(ExceptionBlock node)
		{
			traverse(node.onList);
			traverse(node.@default);
		}
				
		public virtual void Visit(RaiseStatement node)
		{
			traverse(node.lvalue);
			traverse(node.expr);
		}
				
		public virtual void Visit(OnStatement node)
		{
			traverse(node.body);
		}
				
		public virtual void Visit(AssemblerBlock node)
		{
		}
				
		public virtual void Visit(TypeNode node)
		{
		}
				
		public virtual void Visit(UndefinedType node)
		{
		}
				
		public virtual void Visit(DeclaredType node)
		{
		}
				
		public virtual void Visit(RecordType node)
		{
			traverse(node.compTypes);
		}
				
		public virtual void Visit(ProceduralType node)
		{
		}
				
		public virtual void Visit(ClassType node)
		{
		}
				
		public virtual void Visit(VariableType node)
		{
		}
				
		public virtual void Visit(MetaclassType node)
		{
			traverse(node.baseType);
		}
				
		public virtual void Visit(TypeUnknown node)
		{
		}
				
		public virtual void Visit(EnumType node)
		{
			traverse(node.enumVals);
		}
				
		public virtual void Visit(RangeType node)
		{
			traverse(node.min);
			traverse(node.max);
		}
				
		public virtual void Visit(ScalarType node)
		{
		}
				
		public virtual void Visit(StringType node)
		{
		}
				
		public virtual void Visit(FixedStringType node)
		{
			traverse(node.expr);
		}
				
		public virtual void Visit(VariantType node)
		{
			traverse(node.type);
		}
				
		public virtual void Visit(PointerType node)
		{
			traverse(node.pointedType);
		}
				
		public virtual void Visit(IntegralType node)
		{
		}
				
		public virtual void Visit(IntegerType node)
		{
		}
				
		public virtual void Visit(SignedIntegerType node)
		{
		}
				
		public virtual void Visit(UnsignedIntegerType node)
		{
		}
				
		public virtual void Visit(UnsignedInt8Type node)
		{
		}
				
		public virtual void Visit(UnsignedInt16Type node)
		{
		}
				
		public virtual void Visit(UnsignedInt32Type node)
		{
		}
				
		public virtual void Visit(UnsignedInt64Type node)
		{
		}
				
		public virtual void Visit(SignedInt8Type node)
		{
		}
				
		public virtual void Visit(SignedInt16Type node)
		{
		}
				
		public virtual void Visit(SignedInt32Type node)
		{
		}
				
		public virtual void Visit(SignedInt64Type node)
		{
		}
				
		public virtual void Visit(BoolType node)
		{
		}
				
		public virtual void Visit(CharType node)
		{
		}
				
		public virtual void Visit(RealType node)
		{
		}
				
		public virtual void Visit(FloatType node)
		{
		}
				
		public virtual void Visit(DoubleType node)
		{
		}
				
		public virtual void Visit(ExtendedType node)
		{
		}
				
		public virtual void Visit(CurrencyType node)
		{
		}
				
		public virtual void Visit(StructuredType node)
		{
			traverse(node.basetype);
		}
				
		public virtual void Visit(ArrayType node)
		{
		}
				
		public virtual void Visit(SetType node)
		{
		}
				
		public virtual void Visit(FileType node)
		{
		}
	}
}
