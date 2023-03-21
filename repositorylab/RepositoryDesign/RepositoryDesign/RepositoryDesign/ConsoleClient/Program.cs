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
                        moveCourses();
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
            string TeacherName = Console.ReadLine();
            Teacher teacher = new Teacher() { TeacherName = TeacherName };
            teacher.EntityState = EntityState.Added;
            businessLayer.AddTeacher(teacher);
            Console.WriteLine("{0} has been added to the database.", TeacherName);
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
                Console.WriteLine("Enter a teacher's name: ");
                Teacher t = businessLayer.GetTeacherByName(Console.ReadLine());
                if (t != null)
                {
                    Console.WriteLine("Update the teacher's name: ");
                    t.TeacherName = Console.ReadLine();
                    t.EntityState = EntityState.Modified;
                    businessLayer.UpdateTeacher(t);
                }
                else
                {
                    Console.WriteLine("The teacher does not exist.");
                };
            }
            //find by a teacher's id
            else if (input == 2)
            {
                int id = Validator.getId();
                Teacher t = businessLayer.GetTeacherById(id);
                if (t != null)
                {
                    Console.WriteLine("Update the teacher's name: ");
                    t.TeacherName = Console.ReadLine();
                    t.EntityState = EntityState.Modified;
                    businessLayer.UpdateTeacher(t);
                }
                else
                {
                    Console.WriteLine("The teacher does not exist.");
                };
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
            Teacher t = businessLayer.GetTeacherById(id);
            if (t != null)
            {
                Console.WriteLine("{0} has been removed from the database.", t.TeacherName);
                t.EntityState = EntityState.Deleted;
                businessLayer.RemoveTeacher(t);
            }

            else
            {
                Console.WriteLine("The teacher does not exist.");
            };

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
                Console.WriteLine("Teacher ID: {0}, Name: {1}", t.TeacherId, t.TeacherName);
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
            Teacher t = businessLayer.GetTeacherById(id);
            if (t != null)
            {
                Console.WriteLine("Listing courses for Teacher: [ID: {0}, Name: {1}]:", t.TeacherId, t.TeacherName);
                if (t.Courses.Count > 0)
                {
                    foreach (Course c in t.Courses)
                        Console.WriteLine("Course ID: {0}, Name: {1}", c.CourseId, c.CourseName);
                }
                else
                {
                    Console.WriteLine("No courses for Teacher: [ID: {0}, Name: {1}]:", t.TeacherId, t.TeacherName);
                };

            }
            else
            {
                Console.WriteLine("Teacher does not exist.");
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
            Console.WriteLine("Select a teacher for this course. ");
            int id = Validator.getId();
            //Get the teacher object using the id
            //your code
            Teacher t = businessLayer.GetTeacherById(id);

            if (t != null)
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
                Course course = new Course()
                {
                    CourseName = courseName,
                    TeacherId = t.TeacherId,
                    EntityState = EntityState.Added
                };

                t.EntityState = EntityState.Modified;

                foreach (Course c in t.Courses)
                    c.EntityState = EntityState.Unchanged;

                t.Courses.Add(course);
                businessLayer.UpdateTeacher(t);
                Console.WriteLine("{0} has been added to the database.", courseName);
            }
            else
            {
                Console.WriteLine("Teacher does not exist.");
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

            //find course by name
            if (input == 1)
            {
                Console.WriteLine("Enter a course's name: ");
                //Get a course object by name
                //Your code
                Course course = businessLayer.GetCourseByName(Console.ReadLine());
                if (course != null)
                {   //Update the course
                    //Your code
                    //Your code
                    Console.WriteLine("Update this course's name to: ");
                    course.CourseName = Console.ReadLine();
                    course.EntityState = EntityState.Modified;
                    businessLayer.UpdateCourse(course);
                }
                else
                {
                    Console.WriteLine("Course does not exist.");
                };
            }
            //find course by id
            else if (input == 2)
            {
                int id = Validator.getId();
                //Get the course by course id
                //Your code
                Course course = businessLayer.GetCourseById(id);
                if (course != null)
                {  //Update the course
                    //Your code
                    Console.WriteLine("Update this course's name to: ");
                    course.CourseName = Console.ReadLine();
                    course.EntityState = EntityState.Modified;
                    businessLayer.UpdateCourse(course);
                }
                else
                {
                    Console.WriteLine("Course does not exist.");
                };
            }
        }

        /// <summary>
        /// Remove a course in the database.
        /// </summary>
        public static void removeCourse()
        {
            listCourses();
            int id = Validator.getId();
            //Get a Course object by id
            //Your code
            Course course = businessLayer.GetCourseById(id);
            if (course != null)
            {   //Display the message the course name has been removed
                //Remove the course
                //Your code
                Console.WriteLine("{0} has been removed from the database.", course.CourseName);
                course.EntityState = EntityState.Deleted;
                businessLayer.RemoveCourse(course);
            }
            else
            {
                Console.WriteLine("Course does not exist.");
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
                Console.WriteLine("Course ID: {0}, Name: {1}", c.CourseId, c.CourseName);
        }

        public static void addCourseToTeacher()
        {
            listTeachers();
            Console.WriteLine("Select the teacher's [ID] that you want to add the course to: ");

            int id = Validator.getId();
            Teacher t = businessLayer.GetTeacherById(id);

            if (t != null)
            {
                Console.WriteLine("Courses for teacher: [ID: {0}, Name: {1}]:", t.TeacherId, t.TeacherName);
                if (t.Courses.Count > 0)
                {
                    listCourses();

                    Console.WriteLine("Select the Course [ID] you want to add: ");
                    int CourseID = Validator.getId();

                    Course course = businessLayer.GetCourseById(CourseID);

                    if (course.Teacher != null)
                    {
                        Console.WriteLine("Teacher already taken the course.");
                    }
                    else if (course != null)
                    {
                        course.EntityState = EntityState.Modified;
                        course.Teacher = t;
                        course.TeacherId = t.TeacherId;

                        t.EntityState = EntityState.Modified;
                        foreach (Course c in t.Courses)
                            c.EntityState = EntityState.Unchanged;
                        t.Courses.Add(course);
                        businessLayer.UpdateTeacher(t);

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
                Console.WriteLine("Teacher does not exist.");
            }
        }

        public static void moveCourses()
        {
            listTeachers();
            int id1 = Validator.getId();

            Console.WriteLine("Select the Teacher's [ID] you want to get courses from: ");

            Teacher t1 = businessLayer.GetTeacherById(id1);
            if (t1 != null)
            {
                Console.WriteLine("Courses for [ID: {0}, Name: {1}]:", t1.TeacherId, t1.TeacherName);
                if (t1.Courses.Count > 0)
                {
                    foreach (Course c in t1.Courses)
                        Console.WriteLine("Course ID: {0}, Name: {1}", c.CourseId, c.CourseName);

                    Console.WriteLine("Select Course [ID] you want to reassign: ");
                    int CourseID = Validator.getId();
                    Course course = businessLayer.GetCourseById(CourseID);

                    if (course != null)
                    {
                        Console.WriteLine("Select another teacher [ID] for course assignment: ");
                        int id2 = Validator.getId();
                        Teacher t2 = businessLayer.GetTeacherById(id2);

                        if (t2 != null)
                        {
                            t1.EntityState = EntityState.Modified;
                            t1.Courses.Remove(course);

                            foreach (Course c in t1.Courses)
                                c.EntityState = EntityState.Unchanged;

                            businessLayer.UpdateTeacher(t1);

                            course.EntityState = EntityState.Modified;
                            course.Teacher = null;
                            course.TeacherId = null;

                            businessLayer.UpdateCourse(course);

                            course.EntityState = EntityState.Modified;
                            course.Teacher = t2;
                            course.TeacherId = t2.TeacherId;

                            t2.EntityState = EntityState.Modified;

                            foreach (Course c in t2.Courses)
                                c.EntityState = EntityState.Unchanged;

                            t2.Courses.Add(course);

                            businessLayer.UpdateTeacher(t2);
                            businessLayer.UpdateCourse(course);

                            Console.WriteLine("Course reassigned.");
                        }
                        else
                        {
                            Console.WriteLine("Teacher does not exist");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Course does not exist");
                    }

                }
                else
                {
                    Console.WriteLine("No courses for [ID: {0}, Name: {1}]:", t1.TeacherId, t1.TeacherName);
                };

            }
            else
            {
                Console.WriteLine("Teacher does not exist.");
            };
        }

    }
}