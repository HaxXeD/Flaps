function UnityProgress(unityInstance, progress) {
  if (!unityInstance.Module)
    return;
  if (!unityInstance.logo) {
    unityInstance.logo = document.createElement("div");
    unityInstance.logo.className = "logo " + unityInstance.Module.splashScreenStyle;
    unityInstance.container.appendChild(unityInstance.logo);
  }
  if (!unityInstance.progress) {    
    unityInstance.progress = document.createElement("div");
    unityInstance.progress.className = "progress " + unityInstance.Module.splashScreenStyle;
    unityInstance.progress.empty = document.createElement("div");
    unityInstance.progress.empty.className = "empty";
    unityInstance.progress.appendChild(unityInstance.progress.empty);
    unityInstance.progress.full = document.createElement("div");
    unityInstance.progress.full.className = "full";
    unityInstance.progress.appendChild(unityInstance.progress.full);
    unityInstance.container.appendChild(unityInstance.progress);
    let orn = getOrientation();

              
          function getOrientation() {
              let _orn = screen.msOrientation ||
              (screen.orientation || screen.mozOrientation).type;
          
              switch(_orn){
                  case 'portrait-primary':
                  
                  console.log('Phone')
                  document.getElementById('scale').style.display = "none"; 
                  document.getElementById('Fs_screen').style.display = "none"; 
                  document.getElementById('title_screen').style.display = "initial";

                  
                  case 'portrait-secondary':
                  document.getElementById('scale').style.display = "none"; 
                  document.getElementById('Fs_screen').style.display = "none";
                  document.getElementById('title_screen').style.display = "initial";  
                      break;
                  case 'landscape-primary':
                      console.log('This is the laptop/desktop version')
                      document.getElementById('scale').style.display = "initial";
                      document.getElementById('Fs_screen').style.display = "initial";  
                      document.getElementById('title_screen').style.display = "none";
                      break;
                  case 'landscape-secondary':
                  document.getElementById('scale').style.display = "initial";  
                  document.getElementById('Fs_screen').style.display = "initial";   
                  document.getElementById('title_screen').style.display = "none";
                      break;
                  case undefined:
                      //not supported
                      break;
                  default:
                      //something unknown
              }
              return _orn;
          }
         
          window.addEventListener('orientationchange', (ev)=>{
              orn = getOrientation();
              
              
              console.dir(ev);
              
          });
    
  }
  unityInstance.progress.full.style.width = (100 * progress) + "%";
  unityInstance.progress.empty.style.width = (100 * (1 - progress)) + "%";
  if (progress == 1)
    unityInstance.logo.style.display = unityInstance.progress.style.display = "none";
}