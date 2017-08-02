using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_UI.Modules
{
    public class ContextManager
    {
        public IKernel AppKernel { get; set; }
        public List<IKernel> OpenKernels { get; set; } = new List<IKernel>();
       
        private int ActiveKernelIndex { get; set; } = -1;

        public ContextManager([Named("AppKernel")] IKernel kern)
        {
            
            AppKernel = kern;
            AddNewKernel();

        }

        private void AddNewKernel()
        {
            OpenKernels.Add(new StandardKernel(new ProjectModule(this)));
            ActiveKernelIndex = OpenKernels.Count - 1;
        }
        private void RemoveKernel(IKernel k)
        {
            try
            {
                if (OpenKernels.Count > 1)
                {
                    OpenKernels.Remove(k);
                    ActiveKernelIndex = OpenKernels.Count - 1;
                }
                else
                {
                    AddNewKernel();
                    OpenKernels.Remove(k);
                    ActiveKernelIndex = OpenKernels.Count - 1;
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        public IKernel ActiveKernel { get { return OpenKernels[ActiveKernelIndex]; } }
    }
}
