using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace TIC.PuppyLove.Profile.ServicesHost
{
    /// <summary>
    /// This class is used as a custom derivative of the ServiceHost class to alter the run-time behavior of a service. 
    /// </summary>
    /// <remarks></remarks>
    public class HostFactory : ServiceHostFactory
    {
        #region Proctected Methods

        /// <summary>
        /// Creates a System.ServiceModel.ServiceHost for a specified type of service with a specific base address.
        /// This method is used to alter the run-time behavior of a service while creating ServiceHost
        /// </summary>
        /// <param name="serviceType">Specifies the type of service to host</param>
        /// <param name="baseAddresses">The System.Array of type System.Uri that 
        /// contains the base addresses for the service hosted</param>
        /// <returns>Instance of an service host</returns>
        /// <remarks></remarks>
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = new ServiceHost(serviceType, baseAddresses);
            return host;
        }

        #endregion

    }
}