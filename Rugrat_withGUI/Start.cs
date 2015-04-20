using System;
using System.Collections.Generic;
using System.Text;
using System.IO;




namespace edu.uta.cse.proggen.start.Start
{
    using Method = edu.uta.cse.proggen.classLevelElements.Method;
    using ConfigurationXMLParser = edu.uta.cse.proggen.configurationParser.ConfigurationXMLParser;
    using SingleEntryGenerator = edu.uta.cse.proggen.namespaceLevelElements.SingleEntryGenerator;
    using ProgGenUtil = edu.uta.cse.proggen.util.ProgGenUtil;
    using ClassGenerator = edu.uta.cse.proggen.namespaceLevelElements.ClassGenerator;
    using TreeOfSingleEntryGenerator = edu.uta.cse.proggen.namespaceLevelElements.TreeOfSingleEntryGenerator;
    using InterfaceGenerator = edu.uta.cse.proggen.namespaceLevelElements.InterfaceGenerator;
  
	/// <summary>
	/// Starting point of the ProgGen tool.
	/// 
	/// @author Team 6 - CSE6324 - Spring 2015
	/// 
	/// </summary>
	public class Start
	{

		private static string pathToDir = @"C:\cse 6324\rugrat\";

        // For Unit Testing Purpose
	    public static List<ClassGenerator> classGenList;
	    public static bool testingFlag = false;

		public static string PathToDir
		{
			get
			{
				return pathToDir;
			}
		}

        public static void Main1(string[] args)
        {
            /* For ant script: specify 'config.xml' file and output directory */

            if (args.Length == 1)
            {
                pathToDir = args[0] + System.IO.Path.PathSeparator;
                pathToDir = pathToDir.Remove(pathToDir.Length - 1); //To remove the last semi colon
            }

            // This validation is for Unit Testing purpose
            if (args.Length == 2)
            {
                if (args[1].Equals("NUnit"))
                {
                    testingFlag = true;
                }
            }

            /* List of generated class objects: ClassGenerators */
            List<ClassGenerator> list = new List<ClassGenerator>();
            //	List<InterfaceGenerator> interfaceList = new List<InterfaceGenerator>();
            int numberOfClasses = 0;
            int maxInheritanceDepth = 0;
            int noOfInheritanceChains = 0;
            int noOfInterfaces = 0;
            int maxInterfacesToImplement = 0;

            /* Set of generated classes, it's updated in ClassGenerator.generate() */
            HashSet<string> generatedClasses = new HashSet<string>();
            HashSet<string> preGeneratedClasses = new HashSet<string>();

            try
            {
                string className = ConfigurationXMLParser.getProperty("classNamePrefix");
                int totalLoc = ConfigurationXMLParser.getPropertyAsInt("totalLOC");

                numberOfClasses = ConfigurationXMLParser.getPropertyAsInt("noOfClasses");
               
                HashSet<string> classList = new HashSet<string>();

                for (int i = 0; i < numberOfClasses; i++)
                {

                    classList.Add(className + i);
                }
                //E.g., {[2,5,6], [0,1,4]}
              

                for (int i = 0; i < numberOfClasses; i++)
                {
                    // All classes have equal number of variables, methods, etc. Should we change it?	
                    // classes are like A1, A2, etc where A=<UserDefinedName> 
                    // All such cases are handled in the ClassGenerator. It generates arbitrary number of
                    // fields, methods. Only constraint is it should override all the methods of the interfaces
                    // it implements.

                    ClassGenerator test = new ClassGenerator(className + i, totalLoc / numberOfClasses, null);
                    list.Add(test);

                }
                string path = @"C:\cse 6324\rugrat\TestPrograms\edu\uta\cse6324\team6\test";
            
                DirectoryInfo directory = Directory.CreateDirectory(path);
             


                if (!directory.Exists)
                {
                    directory = System.IO.Directory.CreateDirectory(path);
                    Console.WriteLine(directory);
                }
               
            }
            catch (System.FormatException e)
            {
                Console.WriteLine("Please enter integer values for arguments that expect integers!!!");
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                Environment.Exit(1);
            }

            //This copy of List<ClassGenerator> is only used for Unit Testing purpose
            classGenList = list;

            //do pre-generation for classes
            //pre-generation determines the class members variables and method signatures
            foreach (ClassGenerator generator in list)
            {
                generator.preGenerateForMethodSignature(list, preGeneratedClasses);
            }

            // Proceed Writing the content fo file only if it an actual run
            // Skip writing for Unit Testing
            if (testingFlag == false)
            {
                foreach (ClassGenerator generator in list)
                {
                    //How can 'generatedClasses' contain any of the ClassGenerator objects from the list? 
                    //classes are generated semi-recursively. The classes will invoke class generation on the
                    //super class. The current class will be generated only after all its ancestor classes are generated.
                    //We do not want to regenerate the ancestor classses and make stale the information used by its sub-classes
                    //based on the earlier version.
                    if (!generatedClasses.Contains(generator.FileName))
                    {
                        //call generate to construct the class contents
                        generator.generate(list, generatedClasses, preGeneratedClasses);
                    }
                    writeToFile(generator);
                }


                TreeOfSingleEntryGenerator treeSingleEntryGen = new TreeOfSingleEntryGenerator(list, pathToDir);
                treeSingleEntryGen.generateTreeOfSingleEntryClass();

                //write the reachability matrix

                if (ConfigurationXMLParser.getProperty("doReachabilityMatrix").Equals("no"))
                {
                    return;
                }


                List<Method> methodListAll = new List<Method>();
                foreach (ClassGenerator generator in list)
                {
                    methodListAll.AddRange(generator.MethodList);
                }

                StringBuilder builder = new StringBuilder();
                builder.Append("Name, ");

                foreach (Method method in methodListAll)
                {
                    builder.Append(method.AssociatedClass.FileName + "." + method.Name);
                    builder.Append(", ");
                }
                builder.Append("\n");

                foreach (Method method in methodListAll)
                {
                    builder.Append(method.AssociatedClass.FileName + "." + method.Name);
                    builder.Append(", ");
                    foreach (Method calledMethod in methodListAll)
                    {

                        if (
                            method.CalledMethodsWithClassName.Contains(calledMethod.AssociatedClass.FileName + "." +
                                                                       calledMethod.Name))
                        {
                            builder.Append("1, ");
                        }
                        else
                        {
                            builder.Append("0, ");
                        }
                    }
                    builder.Append("\n");
                }
                writeReachabilityMatrix(builder.ToString());
            }

            // Reset Testing Flag
            testingFlag = false;
        }

		private static void writeReachabilityMatrix(string matrix)
		{
			System.IO.FileStream fos = null;
            StreamWriter outstream = new StreamWriter("C:\\log.txt", true) ;

			try
			{
                System.IO.File.WriteAllText(@"C:\cse 6324\rugrat\TestPrograms\edu\uta\cse6324\team6\test\ReachabilityMatrix.csv", matrix);
				Console.WriteLine("Writing reachability matrix...");
               	outstream.WriteLine(matrix.GetBytes());
			}
			catch (Exception e)
			{
				Console.WriteLine("Error writing reachability matrix!");
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
			finally
			{
				try
				{
					if (outstream != null)
					{
                        outstream.Flush();
						outstream.Close();
					}
				}
				catch (IOException e)
				{
					Console.WriteLine("Error closing output filestream");
					Console.WriteLine(e.ToString());
					Console.Write(e.StackTrace);
					Environment.Exit(1);
				}
			}
		}

       private static void writeToFile(ClassGenerator generator)
		{
			if (generator == null)
			{
				return;
			}
			string filename = generator.FileName;
			write(filename, generator.ToString());
		}

		private static void writeToFile(InterfaceGenerator generator)
		{
			if (generator == null)
			{
				return;
			}
			write(generator.Name, generator.ToString());
		}

		private static void write(string filename, string contents)
		{
			System.IO.FileStream fos = null;
            String path = @"C:\cse 6324\rugrat\TestPrograms\edu\uta\cse6324\team6\test\" + filename + ".cs";
            

			try
			{                         
                
                System.IO.File.WriteAllText(path, contents);

				// To let the user know RUGRAT is working...
                Console.WriteLine("I am from Rugrat c#");
				Console.WriteLine("Writing to file...." + filename);
				
				// Successfully written
				Console.WriteLine("Successfully written.");
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine("Error writing out class to .cs file : " + filename);
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				Environment.Exit(1);
			}
			catch (IOException e)
			{
				Console.WriteLine("Error writing out class to .cs file : " + filename);
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				Environment.Exit(1);
			}
			finally
			{
				try
				{
						//
				}
					catch (IOException e)
					{
					Console.WriteLine("Error closing output filestream");
					Console.WriteLine(e.ToString());
					Console.Write(e.StackTrace);
					Environment.Exit(1);
					}
			}
		}
      
	}

}