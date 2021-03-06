# HospitalProject Team 1 for MICs Group of Health Services
### Team Members
* Krishna Shah N01318294
* Chang Qi N01294329 
* Amit Deka N01366094 
* Yash Pathak N01364240 
* Oswaldo Meza N01383041 
* Dhanpreet Singh Bhamra N01403913 

#### Documentation
* There is a folder name : Documentation that has sub-folders with each team member’s name

#### Referance 
*  https://github.com/christinebittle/PetGroomingMVC


## Krishna Shah
-------- Models ---------

* TestimonialModel.cs
* ServiceModel.cs
* BranchModel.cs

#### ViewModel
* ShowBranchModel.cs
* ShowServiceModel.cs
* UpdateBranchServiceModel.cs

-------- View ---------

#### Testimonial
* Create.cshtml (view for the patients to give there testimonial by filling the form)
* DeleteConfirm.cshtml (view for staff to delete the testimonial permanently)
* List.cshtml (staff view, list of all the testimonials of the database) (has pagination)
* Update.cshtml (staff view to update the existig testimonial)
* ThankVisitor.cshtml (Visitor view with a success message after submitting the new testimonial)
* VisitorView.cshtml (visitor view, list of all the testimonials that are published) (has pagination)

#### Service Availibility
* Create.cshtml (form view for staff to create new service and select @available location)
* DeleteConfirm.cshtml (view for staff to delete the service permanently from the database)
* List.cshtml (view for list of all services of all location)
- future scope : to let staff search/filter the services by category wise) 
* Update.cshtml : (view for staff to update the service to add/remove location or name)
* Show.cshtml : (simply view the service for staff)

#### Branch
* VisitorView.cshtml (visitor can see the 3 locations and services offered by the locations)
* List.cshmtl (list of 3 locations with its information)

-------- Controller ---------

* TestimonialController.cs (methods for the crud functionalilites)
* ServicesController.cs (methods for the crud functionalilites)
* BranchController.cs (method for read function)


## Yash Pathak
-------- Models ---------

* ApplicantModel.cs
* DonationModel.cs
* JobModel.cs

#### ViewModel
* ShowApplicant.cs
* UpdateJob.cs

-------- View ---------

#### Applicant
* Create.cshtml
* Delete.cshtml
* Details.cshtml
* List.cshtml
* Update.cshtml


#### Job
* Create.cshtml
* Delete.cshtml
* Details.cshtml
* List.cshtml
* Update.cshtml

#### Donation
* Create.cshtml
* Delete.cshtml
* Details.cshtml
* List.cshtml
* Update.cshtml

-------- Controller ---------

* ApplicantController.cs
* DonationController.cs
* JobController.cs

-------- Miscellaneous ---------
* Error Fixing.
* Conflict Solving.


## Chang Qi
 (To avoid conflicts, worked on personal branch "EllaQi" and didn't merge to the master branch)
 
 (Accidentally clicked 'Update Nuget Packages', then thousands of changes have been made, then the project couldn't load...)  
 
-------- Models ---------

* ContactForm.cs
* ContactStatus.cs
* RespondWay.cs
* UserRegistration.cs
* Role.cs

#### ViewModel
* AddContact.cs
* UpdateContact.cs

-------- View ---------

#### ContactForm
* Create.cshtml
* Delete.cshtml
* Details.cshtml
* List.cshtml
* Update.cshtml

-------- Controller ---------

#### ContactForm
* ContactFormController.cs
* RespondWayController.cs
* ContactStatusController.cs

-------- Additional Duties --------

#### Design
 * Update Views/Home/Index.cshtml (In order to edit index page styles more efficiently and intuitively, some of the code used inline CSS) 
 * Update Content/bootstrap.css
 * Update Content/Site.css
 * Update Views/Shared/_Layout.cshtml
 * Create Home page wireframes: 
    * Documentation/EllaQi/Homepage-Wirerames/
      * wireframe-desktop1.0.png, wireframe-tablet1.0.png, wireframe-phone1.0.png, navigation-clarification.png
 * Add logo, pictures, and icons for index page: 
   * Content/img/
     * mics-logo.png, PhotobyiMattSmartonUnsplash.jpg, facebook-square-brands.svg, twitter-square-brands.svg, envelope-square-solid.svg

#### Other
* Conflict Solving


## Amit Deka
-------- Models ---------

* Doctor.cs
* DocReview.cs
* Speciality.cs
* Faq.cs
* FaqCategory.cs



-------- View ---------
* Doctor
* Speciality
* Faq


-------- Controller ---------
* DoctorController.cs
* SpecialityController.cs
* FaqCategoryController.cs
* FaqController.cs

