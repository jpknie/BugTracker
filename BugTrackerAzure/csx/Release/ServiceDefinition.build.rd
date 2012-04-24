<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="BugTrackerAzure" generation="1" functional="0" release="0" Id="3bbb613d-22ce-4814-a6fc-9764d1a0f2d5" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="BugTrackerAzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="BugTracker:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/LB:BugTracker:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="BugTracker:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/LB:BugTracker:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="BugTracker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/MapBugTracker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="BugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/MapBugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="BugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/MapBugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="BugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/MapBugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="BugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/MapBugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="BugTracker:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/MapBugTracker:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="BugTrackerInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/MapBugTrackerInstances" />
          </maps>
        </aCS>
        <aCS name="Certificate|BugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/MapCertificate|BugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:BugTracker:Endpoint1">
          <toPorts>
            <inPortMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTracker/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:BugTracker:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTracker/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:BugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTracker/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapBugTracker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTracker/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapBugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTracker/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapBugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTracker/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapBugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTracker/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapBugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTracker/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapBugTracker:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTracker/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapBugTrackerInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTrackerInstances" />
          </setting>
        </map>
        <map name="MapCertificate|BugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTracker/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="BugTracker" generation="1" functional="0" release="0" software="C:\Users\Jani\documents\visual studio 2010\Projects\BugTrackerAzure\BugTrackerAzure\csx\Release\roles\BugTracker" entryPoint="base\x86\WaHostBootstrapper.exe" parameters="base\x86\WaWebHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="BugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/SW:BugTracker:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;BugTracker&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;BugTracker&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTracker/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTrackerInstances" />
            <sCSPolicyFaultDomainMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTrackerFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="BugTrackerFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="BugTrackerInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="94ec2653-256f-4a96-a239-6a256002810f" ref="Microsoft.RedDog.Contract\ServiceContract\BugTrackerAzureContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="523b5230-8a71-46eb-9e8d-4501583f51d3" ref="Microsoft.RedDog.Contract\Interface\BugTracker:Endpoint1@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTracker:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="01ad8d6e-96e5-4379-9dba-b42c0314ebb2" ref="Microsoft.RedDog.Contract\Interface\BugTracker:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/BugTrackerAzure/BugTrackerAzureGroup/BugTracker:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>