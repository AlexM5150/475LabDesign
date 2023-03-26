using DomainModel;
using Mm.BusinessLayer;
using System;
using System.Collections.Generic;

namespace ConsoleClient
{
    class Program
    {
        private static IBusinessLayer businessLayer = new BuinessLayer();

        static void Main(string[] args)
        {
            run();
        }

        /// <summary>
        /// Display the menu and get user selection until exit.
        /// </summary>
        public static void run()
        {
            bool repeat = true;
            int input;

            do
            {
                Menu.displayMenu();
                input = Validator.getMenuInput();

                switch (input)
                {
                    case 0:
                        repeat = false;
                        break;
                    case 1:
                        Menu.clearMenu();
                        addTeacher();
                        break;
                    case 2:
                        Menu.clearMenu();
                        updateTeacher();
                        break;
                    case 3:
                        Menu.clearMenu();
                        removeTeacher();
                        break;
                    case 4:
                        Menu.clearMenu();
                        listTeachers();
                        break;
                    case 5:
                        Menu.clearMenu();
                        listTeacherCourses();
                        break;
                    case 6:
                        Menu.clearMenu();
                        addCourse();
                        break;
                    case 7:
                        Menu.clearMenu();
                        updateCourse();
                        break;
                    case 8:
                        Menu.clearMenu();
                        removeCourse();
                        break;
                    case 9:
                        Menu.clearMenu();
                        listCourses();
                        break;
                    case 10:
                        Menu.clearMenu();
                        addCourseToTeacher();
                        break;
                    case 11:
                        Menu.clearMenu();
                        moveCourse();
                        break;
                }
            } while (repeat);
        }

        //CRUD for teachers

        /// <summary>
        /// Add a teacher to the database.
        /// </summary>
        public static void addTeacher()
        {   //YOUR CODE TO ADD A TEACHER THE DATABASE
            //Create a teacher object, set EntityState to Added, add the teacher 
            //object to the database using the businessLayer object, and display
            //a message to the console window that the teacher has been added
            //to the database.
            Console.WriteLine("Enter a teacher's name: ");
            Teacher teacher = new Teacher { EntityState = EntityState.Added, TeacherName = Console.ReadLine() };
            businessLayer.AddTeacher(teacher);
            Console.WriteLine("Teacher {0} has been added to the database", teacher.TeacherName);
        }

        /// <summary>
        /// Update the name of a teacher.
        /// </summary>
        public static void updateTeacher()
        {
            Menu.displaySearchOptions();
            int input = Validator.getOptionInput();
            listTeachers();
            //Find by a teacher's name
            if (input == 1)
            {   //YOUR CODE TO UPDATE A TEACHER THE DATABASE
                //Create a teacher object, input the name of the teacher,
                //and get the teacher by name using a method in the class BusinessLayer.
                //If the teacher object is not null, change the teacher's based on the input user
                //enters, set EntityState to Modified, update the teacher 
                //object to the database using the businessLayer object.
                //If teacher is null, display a message "Teacher does not exist"
                //to the database.
                Console.WriteLine("Enter a teacher's name you want to update: ");
                Teacher teacher = businessLayer.GetTeacherByName(Console.ReadLine());
                if (teacher != null)
                {
                    Console.WriteLine("Enter a new name for the teacher");
                    teacher.TeacherName = Console.ReadLine();
                    teacher.EntityState = EntityState.Modified;
                    businessLayer.UpdateTeacher(teacher);
                    Console.WriteLine("The teacher's name has been sucessfully updated");
                }
                else
                {
                    Console.WriteLine("The teacher does not exist");
                }
            }
            //find by a teacher's id
            else if (input == 2)
            {
                int id = Validator.getId();
                Teacher teacher = businessLayer.GetTeacherById(Validator.getId());
                if (teacher != null)
                {
                    Console.WriteLine("Enter a new name for the teacher");
                    teacher.TeacherName = Console.ReadLine();
                    teacher.EntityState = EntityState.Modified;
                    businessLayer.UpdateTeacher(teacher);
                    Console.WriteLine("The teacher has been sucessfully updated");
                }
                else
                {
                    Console.WriteLine("The teacher does not exist");
                }
            }

            
        }

        /// <summary>
        /// Remove a teacher from the database.
        /// </summary>
        public static void removeTeacher()
        {
            listTeachers();
            int id = Validator.getId();
            //YOUR CODE TO REMOVE A TEACHER THE DATABASE
            //Get the teacher. If the teacher object is not null, display the message that
            //the teacher has been removed. Remove the teacher from the database.
            Teacher teacher = businessLayer.GetTeacherById(id);
            if (teacher != null)
            {
                teacher.EntityState = EntityState.Deleted;
                businessLayer.RemoveTeacher(teacher);
                Console.WriteLine("Teacher {0} has been removed", teacher.TeacherName);
            }
            else
            {
                Console.WriteLine("The teacher does not exist");
            }
            
        }

        /// <summary>
        /// List all teachers in the database.
        /// </summary>
        public static void listTeachers()
        {   //Call a method from the class BusinessLayer to get all the teacher and assign
            //the return to an object of type IList<Teacher>
            //Display the all the teacher id and name.
            //Your code
            IList<Teacher> tList = businessLayer.GetAllTeachers();
            foreach (Teacher t in tList)
            {
                Console.WriteLine("Teacher ID: {0}, Name: {1}", t.TeacherId, t.TeacherName);
            }

        }

        /// <summary>
        /// List the courses of a specified teacher.
        /// </summary>
        public static void listTeacherCourses()
        {
            listTeachers();
            int id = Validator.getId();
            //Get a Teacher object by on the teacher id input
            //If the teacher object is not null
            //   Display teacher id and teacher name
            //   List all the course the teacher teaches
            //Else
            //Display a message " No course for the teacher id and name". Display
            //the teacher's id and name
            Teacher teacher = businessLayer.GetTeacherById(id);
            if (teacher != null)
            {
                if (teacher.Courses.Count > 0)
                {
                    Console.WriteLine("Courses for {0}, ID: {1}", teacher.TeacherName, teacher.TeacherId);
                    foreach (Course c in businessLayer.GetCoursesByTeacherId(id))
                    {
                        Console.WriteLine("Course ID: {0}, Name: {1}", c.CourseId, c.CourseName);
                    }
                }
                else
                {
                    Console.WriteLine("No courses for {0}, ID: {1}", teacher.TeacherName, teacher.TeacherId);
                }

            }
            else
            {
                Console.WriteLine("Th teacher does not exist");
            };
        }

        //CRUD for courses

        /// <summary>
        /// Add a course to a teacher.
        /// </summary>
        public static void addCourse()
        {
            Console.WriteLine("Enter a course name: ");
            string courseName = Console.ReadLine();

            listTeachers();
            Console.WriteLine("Enter a teacher ID for this course: ");
            int id = Validator.getId();
            //Get the teacher object using the id
            //your code
            Teacher teacher = businessLayer.GetTeacherById(id);
            if (teacher != null)
            {
                //create course with name, teacher id, and set entity state to added
                //your code

                //add course to teacher
                //Set the Entity of the teacher object modified
                //Set the entity state of each course from the teacher to Unchanged
                //Add the course to the teacher
                //Update the teacher by calling a method from the class BusinessLayer
                //Display a message the course name has been added to the database.
                //your code
                Course course = new Course{ CourseName = courseName, TeacherId = id, EntityState = EntityState.Added };
                teacher.EntityState = EntityState.Modified;
                foreach (Course c in teacher.Courses)
                {
                    c.EntityState = EntityState.Unchanged;
                }
                teacher.Courses.Add(course);                
                businessLayer.UpdateTeacher(teacher);
                Console.WriteLine("Course {0} successfully added to the database", courseName);           
            }
            else
            {
                Console.WriteLine("The teacher does not exist");
            };
        }

        /// <summary>
        /// Update the name of a course.
        /// </summary>
        public static void updateCourse()
        {
            Menu.displaySearchOptions();
            int input = Validator.getOptionInput();
            listCourses();
            Course course = null;

            //find course by name
            if (input == 1)
            {
                Console.WriteLine("Enter a course name: ");
                //Get a course object by name
                course = businessLayer.GetCourseByName(Console.ReadLine());
              
            }
            //find course by id
            else if (input == 2)
            {
                int id = Validator.getId();
                //Get the course by course id
                course = businessLayer.GetCourseById(id);
            }

            if (course != null)
            {  
                Console.WriteLine("Enter a new course name for {0}: ", course.CourseName);
                course.CourseName = Console.ReadLine();
                course.EntityState = EntityState.Modified;
                businessLayer.UpdateCourse(course);
                Console.WriteLine("Course {0} has been successfully updated", course.CourseName);
            }
            else
            {
                Console.WriteLine("Course does not exist");
            };
        }

        /// <summary>
        /// Remove a course in the database.
        /// </summary>
        public static void removeCourse()
        {
            listCourses();
            int id = Validator.getId();
            //Get a Course object by id
            Course course = businessLayer.GetCourseById(id);
            //Your code
            if (course != null)
            {   //Display the message the course name has been removed
                //Remove the course
                //Your code
                course.EntityState = EntityState.Deleted;
                businessLayer.RemoveCourse(course);
                Console.WriteLine("Course {0} removed successfully", course.CourseName);
            }
            else
            {
                Console.WriteLine("Course does not exist");
            };
        }

        /// <summary>
        /// List all courses in the database.
        /// </summary>
        public static void listCourses()
        {   //List all the courses by id and name
            //Display course id and course name
            //Your code
            IList<Course> cList = businessLayer.GetAllCourses();
            foreach (Course c in cList)
            {
                Console.WriteLine("Course: {0}, ID: {1}.", c.CourseName, c.CourseId);
            }
        }

        public static void addCourseToTeacher()
        {
            listTeachers();
            Console.WriteLine("Enter the teacher's ID you want to add the course to: ");

            int id = Validator.getId();
            Teacher teacher = businessLayer.GetTeacherById(id);

            if (teacher != null)
            {
                Console.WriteLine("Courses for {0}, ID: {1}", teacher.TeacherName, teacher.TeacherId);
                if (teacher.Courses.Count > 0)
                {
                    listCourses();
                    Console.WriteLine("Enter the ID of the course you want to add: ");
                    int CourseID = Validator.getId();
                    Course course = businessLayer.GetCourseById(CourseID);

                    if (course.Teacher != null)
                    {
                        Console.WriteLine("The teacher is already teaching this course.");
                    }
                    else if (course != null)
                    {
                        course.EntityState = EntityState.Modified;
                        course.Teacher = teacher;
                        course.TeacherId = teacher.TeacherId;
                        teacher.EntityState = EntityState.Modified;
                        foreach (Course c in teacher.Courses) {
                            c.EntityState = EntityState.Unchanged;
                        }
                        teacher.Courses.Add(course);
                        businessLayer.UpdateTeacher(teacher);
                        businessLayer.UpdateCourse(course);
                    }
                    else
                    {
                        Console.WriteLine("Course does not exist");
                    }
                }
            }
            else
            {
                Console.WriteLine("The teacher does not exist.");
            }
        }


        public static void moveCourse()
        {
            Menu.displaySearchOptions();
            int input = Validator.getOptionInput();
            listCourses();
            Course course = null;

            //find course by name
            if (input == 1)
            {
                Console.WriteLine("Enter a course name: ");
                course = businessLayer.GetCourseByName(Console.ReadLine());
              
            }
            //find course by id
            else if (input == 2)
            {
                int id = Validator.getId();
                course = businessLayer.GetCourseById(id);
            }

            if (course != null)
            {   //move the course to another teacher
                listTeachers();
                Console.WriteLine("Select a teacher ID for this course: ");
                int id = Validator.getId();
                Teacher teacher = businessLayer.GetTeacherById(id);
                if (teacher != null)
                {
                    course.EntityState = EntityState.Modified;
                    course.Teacher = teacher;                
                    businessLayer.UpdateCourse(course);
                    Console.WriteLine("Course {0} successfully reassigned to {1}.", course.CourseName, teacher.TeacherName);           
                }
                else
                {
                    Console.WriteLine("The teacher does not exist");
                };
            }
            else
            {
                Console.WriteLine("Course does not exist");
            };
        }
    }
}