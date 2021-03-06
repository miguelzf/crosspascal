################################################################################
# Generate Visitor processing methods, from a C# source file with the Nodes' classes
################################################################################

# Commandline and configurable parameters

if ARGV.size < 1
	puts "Must specify Nodes C# source file"
	exit
end

$genfile="ProcessorGenerated.cs"
$genclassname="Processor"
$genbasevisitor = true
$visitreturntype = "bool"

$srcfilename=ARGV[0]
if !File.exist?($srcfilename)
	puts "File " + $srcfilename + " not found"
	exit
end


################################################################################
# Methods

ASTNode = Struct.new(:name, :fields, :base) do
	def inspect 
		self.name + ":" + self.base + "=\n\t" << self.fields.join("\n\t") << "\n"
	end
	
	def print
		puts self.inspect + "\n"
	end
end

def gen_return_stmt(arrcode, prefix)
	if $visitreturntype == "bool" || $visitreturntype == "Boolean" 
		arrcode << prefix+"return true;"
	elsif $visitreturntype == "Object"
		arrcode << prefix+"return null;"
	end
end


def gen_code(nodes, gen_method, isbase)
	arrcode = []
	arrcode << "// ---------------------------------------------------------------------"
	arrcode << "// Base Processor class, auto-generated									"
	arrcode << "// 	Do NOT edit this file												"
	arrcode << "// 	Additional methods should be defined in another file				"
	arrcode << "// ---------------------------------------------------------------------"
	arrcode << ""
	arrcode << "using System;"
	arrcode << "using System.Collections.Generic;"
	arrcode << "using System.Text;"
	arrcode << "using crosspascal.ast.nodes;"
	arrcode << ""
	arrcode << "namespace crosspascal.ast"
	arrcode << "{"
	if isbase
		arrcode << "\t" + "public abstract partial class " + $genclassname
	elsif
		arrcode << "\t" + "public class " + $genclassname + " : Processor"
	end
	arrcode << "\t{"
	arrcode << "\t\t//	Complete interface to be implemented by any specific AST processor	"

	nodesNames= nodes.values.map { |x| x.name }						# do not visit generic types and the base Node class
	codeLines = nodes.values.map { |x| gen_concrete_visit(x,nodesNames) if !(/</ =~ x.name) and x.name != "Node"  }
	codeLines.each do |x| x.insert(0, "") if x != nil end
	text = codeLines.join("\n\t\t")
	arrcode << text
	
	arrcode << "\t}"
	arrcode << "}"
	arrcode 
end

def gen_abstract_visit(node,names)
	cname = node.name
	cfields = node.fields
	arrcode = []
	arrcode << "public abstract "+ $visitreturntype +" Visit(" + cname  + " node);"
	arrcode 
end

def gen_warning_visit(node,names)
	cname = node.name
	cfields = node.fields
	arrcode = []
	arrcode << "public virtual "+ $visitreturntype +" Visit(" + cname  + " node)"
	arrcode << "{"
	arrcode << "\tConsole.Error.WriteLine(\"Visit("+cname + ") not yet implemented.\");"
	gen_return_stmt(arrcode, "\t")
	arrcode << "}"
	arrcode 
end

def gen_concrete_visit(node,names)
	cname = node.name
	cfields = node.fields
	bname = node.base
	
	arrcode = []
	arrcode << "public virtual "+ $visitreturntype +" Visit(" + cname  + " node)"
	arrcode << "{"
	
	# call visit of the baseclass on the current node, if the baseclass is a node
	if bname != '' and names.include? bname
		arrcode << "\t" + "Visit((" + bname +") node);";
	end

	# iterate over lists' nodes
	if cname.end_with? "List"
		arrcode << "\t" + "foreach (Node n in node.nodes)"
		arrcode << "\t\t" + "traverse(n);"
	end
		
	for field in cfields
		type,name = field.split(' ')[0,2];
		puts field + " of " + type
		
	#	if !(/<|>/ =~ type) 	# do not visit generic types
		if names.include? type 
			arrcode << "\t" + "traverse(node." + name + ");"
		
		elsif ! (cname.end_with? "List")
		# iterate over enumerable generic arguments
			if (ma = /\w*List<(\w+)>\s*/.match type) == nil
				if (ma = /\w*Set<(\w+)>\s*/.match type) == nil
					ma = /(\w+)\[\]\s*/.match type		# Array 
				end
			end
			if ma != nil and names.include? ma[1]	# if generic type arg is a Node
				arrcode << "\t" + "foreach (Node n in node." + name + ")"
				arrcode << "\t\t" + "traverse(n);"
			end
		end
	end
	
	gen_return_stmt(arrcode, "\t")
	arrcode << "}"
	arrcode 
end


def process_class(name,body)
	# remove all methods bracket-enclosed bodies
	begin 
		ret = body.gsub! /{[^{}]*}/, ';'
	end while ret != nil
	
	# remove methods
	rgxDecl	= '[<>.\w]+\s+[.<>\w]+\s*\([^)]*\)'
	rgxExt	= ':\s*\w+\s*\([^;]*'
	body.gsub! Regexp.new(rgxDecl+'\s*('+rgxExt+')?\s*;'), ';'

	# remove initializations
	body.gsub!(/\s*=.*?;/, ';')

	fields = body.split(';')	
#	fields.each do |x| print x+ "\n" end
	fields.each do |x| x.strip! end

	# remove static fields
	staticqualifs = ['static', 'const', 'readonly']
	fields.each do |x| staticqualifs.each do |y| x.gsub! Regexp.new(y+'\s+.*'), ''  end end 
	
	# cleanup spurious qualifiers
	qualifs = ['public', 'private', 'protected', 'internal', 'virtual', 'abstract',
				'internal', 'partial', 'sealed', 'override', 'unsafe']
	fields.each do |x| qualifs.each do |y| x.gsub! Regexp.new(y+'\s*'), ''  end end 
	fields.reject!(&:empty?)
	
	# puts fields.map().inject('') {|x,y| x + y}
	fields
end


################################################################################
# Main

nodelines = []
if File.directory?($srcfilename)
	dirpath = $srcfilename.dup
	dirpath.gsub!(File::ALT_SEPARATOR, File::SEPARATOR)
	dirpath += File::SEPARATOR
#	dirfiles = Dir.glob(dirpath+"**/*.cs")
	# Specific file order
	dirfiles = ["Nodes", "Sections", "Declarations", "Routines", "Composites", "Statements", "Expressions", "Types"]
	dirfiles = dirfiles.map {|x| dirpath+x+".cs" }
	nodelines = dirfiles.map {|x| File.readlines(x) }
	nodelines.flatten!

	# Do not accept files now
#else	# is file
#	dirpath = File.dirname($srcfilename)
#	nodelines = File.readlines($srcfilename)
end

genfilename = dirpath+File::SEPARATOR+".."+File::SEPARATOR+$genfile
nodelines.each { |x| x.strip! }

nodes = Hash.new


for i in 0 ... nodelines.size
	line = nodelines[i]
	
	if line.length < 5
		next
	end

	qualifs = '(\s*public)?(\s+internal)?(\s+abstract)?(\s+partial)?'
	if Regexp.new('^'+qualifs+'\s*class\s+(\w+)').match(line) == nil
		next
	end
		
	ma = /class\s+(\w+)(.*)/.match line

	if (ma[2][0] == '<') 	# ignores classes with generics

		# Enter class to process
		classname = ma[1]	# get 1st match result
		classtext = ''
		level = 0
		
		basema = /:\s*([<>.\w]+)/.match(ma[2])
		if basema != nil
			basename = basema[1]
		else
			basename = ''
		end
		
		if (/{/ =~ line ) != nil
			line = /({.*)/.match(ma[2])[1]		# '{' after class line
		else
			i += 1
			line = nodelines[i]
		end
		
		while i < nodelines.size
			line.gsub! /\/\/.*/, ''		# remove 1-line comments
			classtext += line
			if ( /{/ =~ line ) != nil
				level += 1		# open block
			end
			if ( /}/ =~ line ) != nil
				level -= 1	# close  block
				if level == 0
					break
				end
			end
			i += 1
			line = nodelines[i]
		end
		classtext = classtext[1,classtext.size-2]
		
		classtext.gsub! /\/\*.*?\*\//, ''	# remove block comments
		
		fields = process_class(classname, classtext);
		node = ASTNode.new(classname,fields,basename)
		#node.print
		if	nodes[classname] != nil
			nodes[classname].fields.concat fields
			puts 'Node ' + classname + ' already exists'
		elsif
			nodes[classname] = node
		end
	end
end


genmethod = method(:gen_concrete_visit)	# to generate Visitors/Processors with virtual methods that traverse each field
#genmethod = method(:gen_abstract_visit))	# to generate Visitors/Processors with abstract methods without body

output = gen_code nodes,genmethod,$genbasevisitor

#File.open(genfilename, "w").puts(output)

